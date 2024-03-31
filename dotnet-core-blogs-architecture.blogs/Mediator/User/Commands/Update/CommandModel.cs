using MediatR;
using DT.Shared.Web.Results;
using DT.Identity.Core.User.Commands.Shared;

namespace DT.Identity.Core.User.Commands.Update;
public class CommandModel : UserBaseCommandModel, IRequest<ValidationResult>
{
	public int Id { get; set; }
}
