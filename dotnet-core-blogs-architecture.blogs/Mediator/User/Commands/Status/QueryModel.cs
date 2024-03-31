using DT.Shared.Web.Results;
using MediatR;

namespace DT.Identity.Core.User.Commands.Status;
public class QueryModel : IRequest<ValidationResult>
{
	public int Id { get; set; }
}

