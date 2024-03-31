using dotnet_core_blogs_architecture.blogs.Common.Validators;
using FluentValidation;

namespace dotnet_core_blogs_architecture.blogs.Mediator.User.Queries.List;
public class Validator : PageQueryValidator<QueryModel>
{
	public Validator() : base()
	{

		RuleFor(x => x.SortCol)
			.Must(x => new[] { "Name", "Status" }.Contains(x))
			.WithMessage("Sorting is applicable for 'Name' and 'Status'")
			.MaximumLength(50)
			.When(x => !string.IsNullOrWhiteSpace(x.SortCol));
	}
}
