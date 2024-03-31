using dotnet_core_blogs_architecture.infrastructure;
using FluentValidation;

namespace dotnet_core_blogs_architecture.blogs.Common.Validators;
public class PageQueryValidator<T> : AbstractValidator<T>  where T : PageQueryModel
{
	public PageQueryValidator()
	{
		var sortOrderValues = new List<string>() { "ASC", "DESC" };

		RuleFor(x => x.Page).GreaterThanOrEqualTo(0);
		RuleFor(x => x.PageSize).GreaterThanOrEqualTo(1);
		RuleFor(x => x.Search).MaximumLength(100);
		RuleFor(x => x.SortCol).MaximumLength(100);
		RuleFor(x => x.SortOrder).Must(x => sortOrderValues.Contains(x.ToUpper())).When(x => !string.IsNullOrWhiteSpace(x.SortOrder));
	}
}
