using dotnet_core_blogs_architecture.blogs.Common;
using dotnet_core_blogs_architecture.infrastructure.Interfaces;
using FluentValidation;
using System.Text.RegularExpressions;

namespace dotnet_core_blogs_architecture.blogs.Mediator.Post.Commands.Shared;
public abstract class PostBaseValidator<T> : AbstractValidator<T> where T : PostBaseCommandModel
{
    protected readonly IReadRepository<Data.Models.Post> postRepository;
    protected readonly Regex onlyAllowWords = new Regex(RegexConstants.OnlyAllowWords);

    public PostBaseValidator(IReadRepository<Data.Models.Post> postRepository)
    {
        this.postRepository = postRepository;
        RuleFor(x => x).NotEmpty().NotNull();
        RuleFor(x => x.Content).NotEmpty().NotNull().MinimumLength(1).MaximumLength(200);
        RuleFor(x => x.Title).NotEmpty().NotNull().MinimumLength(1).MaximumLength(50);
    }
}
