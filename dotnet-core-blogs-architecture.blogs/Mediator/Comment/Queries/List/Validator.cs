using FluentValidation;
using System.Linq;

namespace dotnet_core_blogs_architecture.blogs.Mediator.Comment.Queries.List
{
    public class Validator : AbstractValidator<QueryModel>
    {
        public Validator()
        {
            RuleFor(x => x.SortCol)
                .Must(x => new[] {"content" }.Contains(x.ToLower()))
                .WithMessage("Sorting is applicable for 'content'")
                .MaximumLength(50)
                .When(x => !string.IsNullOrWhiteSpace(x.SortCol));
        }
    }
}
