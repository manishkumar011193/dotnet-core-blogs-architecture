using dotnet_core_blogs_architecture.blogs.Mediator.User.Commands.Shared;
using dotnet_core_blogs_architecture.Data.Results;
using MediatR;

namespace dotnet_core_blogs_architecture.blogs.Mediator.User.Commands.Create;
public class CommandModel :  UserBaseCommandModel, IRequest<ValidationResult>
{
	public string Email { get; set; }	
}

