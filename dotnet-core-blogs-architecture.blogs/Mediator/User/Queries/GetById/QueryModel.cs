using DT.Shared.Web.Results;
using MediatR;

namespace DT.Identity.Core.User.Queries.GetById;
public class QueryModel : IRequest<ValidationResult>
{
	public int Id { get; set; }
}
