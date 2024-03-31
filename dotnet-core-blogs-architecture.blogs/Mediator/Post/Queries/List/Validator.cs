using FluentValidation;
using System.Linq;

namespace dotnet_core_blogs_architecture.blogs.Mediator.Post.Queries.List
{
    public class Validator : AbstractValidator<QueryModel>
    {
        public Validator()
        {
            RuleFor(x => x.SortCol)
                .Must(x => new[] { "title", "content" }.Contains(x.ToLower()))
                .WithMessage("Sorting is applicable for 'title' and 'content'")
                .MaximumLength(50)
                .When(x => !string.IsNullOrWhiteSpace(x.SortCol));
        }
    }
}
