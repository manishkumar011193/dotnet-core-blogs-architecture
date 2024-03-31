using dotnet_core_blogs_architecture.Data;
using dotnet_core_blogs_architecture.Data.Results;
using dotnet_core_blogs_architecture.infrastructure;
using MediatR;

namespace dotnet_core_blogs_architecture.blogs.Mediator.User.Queries.List;
public class QueryModel : PageQueryModel, IRequest<ValidationResult>
{
	public string LocFilter { get; set; }
	public string DepFilter { get; set; }
	public string DesFilter { get; set; }
	public string TypeFilter { get; set; }
}
