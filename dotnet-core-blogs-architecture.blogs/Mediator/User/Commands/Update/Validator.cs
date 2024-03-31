using dotnet_core_blogs_architecture.blogs.Mediator.User.Commands.Shared;
using dotnet_core_blogs_architecture.infrastructure.Interfaces;
using FluentValidation;

namespace dotnet_core_blogs_architecture.blogs.Mediator.User.Commands.Update;
public class Validator : UserBaseValidator<CommandModel>
{
	public Validator(IReadRepository<Data.Models.User> userRepository)
		: base(userRepository)
	{
		RuleFor(query => query).NotEmpty().NotNull();
		RuleFor(x => x.Id).NotEmpty().NotNull().GreaterThan(0);		
	}
}
