using dotnet_core_blogs_architecture.Data;
using dotnet_core_blogs_architecture.Data.Results;
using MediatR;

namespace dotnet_core_blogs_architecture.blogs.Mediator.Post.Queries.List;
public class QueryModel : PageQueryModel, IRequest<ValidationResult>
{
}
