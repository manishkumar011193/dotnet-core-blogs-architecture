using DT.Identity.Core.User.Commands.Shared;
using DT.Identity.Repository.Models;
using DT.Identity.Repository.Specifications.User;
using DT.Shared.Repository.Interfaces;
using FluentValidation;

namespace DT.Identity.Core.User.Commands.Update;
public class Validator : UserBaseValidator<CommandModel>
{
	public Validator(IReadRepository<Repository.Models.User> userRepository, IReadRepository<DT.Identity.Repository.Models.Country> countryRepository,
		 IReadRepository<UserType> userTypeRepository,
		IReadRepository<Role> roleRepository,
		IReadRepository<Repository.Models.Designation> designationRepository, IReadRepository<Repository.Models.Department> departmentRepository)
		: base(userRepository, countryRepository, userTypeRepository, roleRepository, designationRepository, departmentRepository)
	{
		RuleFor(query => query).NotEmpty().NotNull();
		RuleFor(x => x.Id).NotEmpty().NotNull().GreaterThan(0);
		RuleFor(x => x).Custom(MobileValidator).When(x => x.Mobile != null && x.Mobile.Length > 0);
	}
	private void MobileValidator(CommandModel userUpdateData, ValidationContext<CommandModel> context)
	{
		if (this.userRepository.Any(new UserWithMobileSpecification(userUpdateData.Mobile, userUpdateData.Id)))
		{
			context.AddFailure("Mobile", "Mobile Already Exsist!");
		}
	}
}
