using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DT.Identity.Core.User.Queries.GetById;
using FluentValidation;

namespace DT.Identity.Core.User.Commands.Status;
public class Validator : AbstractValidator<QueryModel>
{
	public Validator()
	{
		RuleFor(query => query).NotEmpty().NotNull();
		RuleFor(x => x.Id).NotEmpty().NotNull().GreaterThan(0);
	}
}
