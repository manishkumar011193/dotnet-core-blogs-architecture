using dotnet_core_blogs_architecture.blogs.Mediator.User.Commands.Shared;
using dotnet_core_blogs_architecture.Data.Results;
using MediatR;

namespace dotnet_core_blogs_architecture.blogs.Mediator.User.Commands.Update;
public class CommandModel : UserBaseCommandModel, IRequest<ValidationResult>
{
	public int Id { get; set; }
}
