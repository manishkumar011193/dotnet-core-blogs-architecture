using dotnet_core_blogs_architecture.blogs.Mediator.Comment.Commands.Shared;
using dotnet_core_blogs_architecture.blogs.Mediator.Post.Commands.Shared;
using dotnet_core_blogs_architecture.Data.Results;
using MediatR;

namespace dotnet_core_blogs_architecture.blogs.Mediator.Comment.Commands.Create;
public class CommandModel : Shared.CommentBaseCommandModel, IRequest<ValidationResult>
{
}
