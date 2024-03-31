using dotnet_core_blogs_architecture.Data.Results;
using DT.Identity.Core.User.Commands.Shared;
using MediatR;

namespace dotnet_core_blogs_architecture.blogs.Mediator.User.Commands.Create;
public class CommandModel :  UserBaseCommandModel, IRequest<ValidationResult>
{
	public string Email { get; set; }	
}

