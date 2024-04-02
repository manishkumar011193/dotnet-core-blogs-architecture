using dotnet_core_blogs_architecture.blogs.Mediator.Post.Commands.Shared;
using dotnet_core_blogs_architecture.blogs.Specification;
using dotnet_core_blogs_architecture.infrastructure.Interfaces;
using FluentValidation;

namespace dotnet_core_blogs_architecture.blogs.Mediator.Post.Commands.Create;
public class Validator : PostBaseValidator<CommandModel>
{
    public Validator(IReadRepository<Data.Models.Post> postRepository) :
        base(postRepository)
    {
        RuleFor(x => x.Title).Custom(PostValidator);
    }
    private void PostValidator(string post, ValidationContext<CommandModel> context)
    {
        if (!string.IsNullOrEmpty(post) && postRepository.Any(new PostFetchSpecification(post)))
        {
            context.AddFailure("Post", "The Post already exist!");
        }
    }
}