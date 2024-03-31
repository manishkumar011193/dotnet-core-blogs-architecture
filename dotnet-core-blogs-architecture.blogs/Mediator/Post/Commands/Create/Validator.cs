using FluentValidation;

namespace dotnet_core_blogs_architecture.blogs.Mediator.Post.Commands.Create;
public class Validator : AbstractValidator<CommandModel>
{
    public Validator()
    {
        RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required.");
        RuleFor(x => x.Content).NotEmpty().WithMessage("Content is required.");
    }
}