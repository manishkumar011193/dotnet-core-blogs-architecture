using FluentValidation;

namespace DT.Identity.Core.User.Queries.GetById;
public class Validator : AbstractValidator<QueryModel>
{
	public Validator()
	{
		RuleFor(query => query).NotEmpty().NotNull();
		RuleFor(x => x.Id).NotEmpty().NotNull().GreaterThan(0);
	}
}
