using DT.Shared.Repository;
using DT.Shared.Web.Results;
using MediatR;

namespace DT.Identity.Core.User.Queries.List;
public class QueryModel : PageQueryModel, IRequest<ValidationResult>
{
	public string LocFilter { get; set; }
	public string DepFilter { get; set; }
	public string DesFilter { get; set; }
	public string TypeFilter { get; set; }
}
