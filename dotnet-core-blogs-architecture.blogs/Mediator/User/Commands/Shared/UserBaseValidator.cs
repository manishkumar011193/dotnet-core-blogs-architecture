using System.Text.RegularExpressions;
using DT.Identity.Core.Common;
using DT.Identity.Repository.Common;
using DT.Identity.Repository.Models;
using DT.Identity.Repository.Specifications.Country;
using DT.Identity.Repository.Specifications.Department;
using DT.Identity.Repository.Specifications.Designation;
using DT.Identity.Repository.Specifications.Role;
using DT.Identity.Repository.Specifications.User;
using DT.Identity.Repository.Specifications.UserType;
using DT.Shared.Repository.Interfaces;
using FluentValidation;

namespace DT.Identity.Core.User.Commands.Shared;
public abstract class UserBaseValidator<T> : AbstractValidator<T> where T : UserBaseCommandModel
{
	protected readonly IReadRepository<Repository.Models.User> userRepository;
	protected readonly Regex donotAllowSplChars = new Regex(RegexConstants.DonotAllowSplChars);
	protected readonly Regex onlyNumerics = new Regex(RegexConstants.OnlyNumerics);
	protected readonly Regex mobilePattern = new Regex(RegexConstants.MobilePattern);
	protected readonly IReadRepository<DT.Identity.Repository.Models.Country> _countryRepository;
	protected readonly IReadRepository<UserType> _userTypeRepository;
	protected readonly IReadRepository<Role> _roleRepository;
	protected readonly IReadRepository<Designation> _designationRepository;
	protected readonly IReadRepository<Department> _departmentRepository;

	public UserBaseValidator(IReadRepository<Repository.Models.User> userRepository, IReadRepository<DT.Identity.Repository.Models.Country> countryRepository,
		 IReadRepository<UserType> userTypeRepository,
		IReadRepository<Role> roleRepository,
		IReadRepository<Designation> designationRepository, IReadRepository<Department> departmentRepository)
	{
		this.userRepository = userRepository;
		_countryRepository = countryRepository;
		_roleRepository = roleRepository;
		_userTypeRepository = userTypeRepository;
		_designationRepository = designationRepository;
		_departmentRepository = departmentRepository;
		RuleFor(x => x.FirstName).NotEmpty().NotNull().MaximumLength(50).Matches(donotAllowSplChars).WithMessage("First name cannot contain special characters.");
		RuleFor(x => x.MiddleName).MaximumLength(50).Matches(donotAllowSplChars).WithMessage("Middle name cannot contain special characters.");
		RuleFor(x => x.LastName).NotEmpty().NotNull().MaximumLength(50).Matches(donotAllowSplChars).WithMessage("Last name cannot contain special characters.");
		RuleFor(x => x.Mobile).Matches(mobilePattern).WithMessage("Please enter a valid 10 digit mobile number").When(x => x.Mobile != null && x.Mobile.Length > 0);
		RuleFor(x => x.Location).NotEmpty().NotNull().MaximumLength(50);
		RuleFor(x => x.DepartmentId).NotNull().NotEmpty().GreaterThan(0).WithMessage("Please select the department of the employee").Custom(DepartmentIdValidator).When(x => x.UserType.HasValue && x.UserType == UserTypeConstants.Employee);
		RuleFor(x => x.Designation).NotNull().NotEmpty().WithMessage("Please select the designation of the employee").When(x => x.UserType.HasValue && x.UserType == UserTypeConstants.Employee);
		RuleFor(x => x.ReportingManagerId).NotNull().GreaterThan(0).WithMessage("Reporting Manager cannot be empty for employee").Custom(ReportingManagerIdValidator).When(x => x.UserType.HasValue && x.UserType == UserTypeConstants.Employee);
		RuleFor(x => x.CountryId).NotNull().GreaterThan(0).Custom(CountryIdValidator);
		RuleFor(x => x.UserType).NotNull().GreaterThan(0).Custom(UserTypeValidator);
		RuleFor(x => x.UserRoles).Must(x => x.All(x => int.TryParse(x.ToString(), out _) && x > 0)).Custom(UserRolesValidator).When(x => x.UserRoles != null);
		RuleFor(x => x.UserRoles).NotNull().NotEmpty().WithMessage("Please select the User Role for Employee").When(x => x.UserType.HasValue && x.UserType == UserTypeConstants.Employee && x.UserRoles == null);
		RuleFor(x => x.Designation.Name).NotNull().NotEmpty().MaximumLength(50).WithMessage("Please Enter The Designation for the current employee").Custom(DesignationNameValidator).When(x => x.UserType.HasValue && x.UserType == UserTypeConstants.Employee && x.Designation != null && x.Designation.Id == null);
		RuleFor(x => x.Designation.Id).Custom(DesignationIdValidator).When(x => x.UserType.HasValue && x.UserType == UserTypeConstants.Employee && x.Designation != null && x.Designation.Id != null);
	}

	protected void CountryIdValidator(int? countryId, ValidationContext<T> context)
	{
		if (countryId.HasValue && countryId.Value > 0 && !_countryRepository.Any(new CountryFetchSpecification(countryId.Value)))
		{
			context.AddFailure("CountryId", "Given Country Id Doesn't Exsist!");
		}
	}

	protected void UserRolesValidator(List<int> roles, ValidationContext<T> context)
	{
		if (roles != null && roles.Count > 0)
		{
			var dbRoles = _roleRepository.List(new RoleWithMultipleIdSpecification(roles.ToArray()));
			roles.ForEach(r =>
			{
				if (!dbRoles.Any(d => d.Id == r))
				{
					context.AddFailure("UserRoles", $"Given User Role Id - {r} Doesn't Exsist!");
				}
			});
		}
	}

	protected void UserTypeValidator(int? userType, ValidationContext<T> context)
	{

		if (userType.HasValue && userType.Value > 0 && !_userTypeRepository.Any(new UserTypeWithIdSpecification(userType.Value)))
		{
			context.AddFailure("UserType", "Given User Type Doesn't Exsist!");
		}
	}

	protected void ReportingManagerIdValidator(int? reportingManagerId, ValidationContext<T> context)
	{
		if (reportingManagerId != null && !userRepository.Any(new ReportingManagerWithSpecification(reportingManagerId.Value)))
		{
			context.AddFailure("ReportingManagerId", "Given Reporting Manager Id Doesn't Exsist, or, The User being specified is not a Manager!");
		}
	}

	protected void DesignationIdValidator(int? designationId, ValidationContext<T> context)
	{

		if (designationId != null && !_designationRepository.Any(new DesignationWithIdSpecification(designationId.Value)))
		{
			context.AddFailure("Designation.Id", "Given Designation Id Doesn't Exsist!");
		}
	}

	protected void DepartmentIdValidator(int? departmentId, ValidationContext<T> context)
	{

		if (departmentId != null && !_departmentRepository.Any(new DepartmentWithIdSpecification(departmentId.Value)))
		{
			context.AddFailure("DepartmentId", "Given Department Id Doesn't Exsist!");
		}
	}

	protected void DesignationNameValidator(string designationName, ValidationContext<T> context)
	{
		if (_designationRepository.Any(new DesignationWithNameSpecification(designationName)))
		{
			context.AddFailure("Designation.Name", "Designation Already Exsist!");
		}
	}
}
