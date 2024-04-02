using FluentValidation;

namespace dotnet_core_blogs_architecture.blogs.Mediator.Post.Commands.Status;
public class Validator : AbstractValidator<QueryModel>
{
    public Validator()
    {
        RuleFor(query => query).NotEmpty().NotNull();
        RuleFor(x => x.Id).NotEmpty().NotNull().GreaterThan(0);
    }
}
