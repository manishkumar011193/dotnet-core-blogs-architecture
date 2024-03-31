using dotnet_core_blogs_architecture.blogs.Common;
using dotnet_core_blogs_architecture.infrastructure.Interfaces;
using FluentValidation;
using System.Text.RegularExpressions;

namespace dotnet_core_blogs_architecture.blogs.Mediator.User.Commands.Shared;
public abstract class UserBaseValidator<T> : AbstractValidator<T> where T : UserBaseCommandModel
{
	protected readonly IReadRepository<Data.Models.User> userRepository;
	protected readonly Regex donotAllowSplChars = new Regex(RegexConstants.DonotAllowSplChars);
	protected readonly Regex onlyNumerics = new Regex(RegexConstants.OnlyNumerics);
	protected readonly Regex mobilePattern = new Regex(RegexConstants.MobilePattern);	

	public UserBaseValidator(IReadRepository<Data.Models.User> userRepository)
	{
		this.userRepository = userRepository;
		
		RuleFor(x => x.FirstName).NotEmpty().NotNull().MaximumLength(50).Matches(donotAllowSplChars).WithMessage("First name cannot contain special characters.");
		RuleFor(x => x.MiddleName).MaximumLength(50).Matches(donotAllowSplChars).WithMessage("Middle name cannot contain special characters.");
		RuleFor(x => x.LastName).NotEmpty().NotNull().MaximumLength(50).Matches(donotAllowSplChars).WithMessage("Last name cannot contain special characters.");
		RuleFor(x => x.Mobile).Matches(mobilePattern).WithMessage("Please enter a valid 10 digit mobile number").When(x => x.Mobile != null && x.Mobile.Length > 0);		
	}
}
