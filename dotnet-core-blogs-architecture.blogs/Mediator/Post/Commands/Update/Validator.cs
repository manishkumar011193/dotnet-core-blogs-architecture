using dotnet_core_blogs_architecture.blogs.Mediator.Post.Commands.Shared;
using dotnet_core_blogs_architecture.blogs.Specification;
using dotnet_core_blogs_architecture.infrastructure.Interfaces;
using FluentValidation;

namespace dotnet_core_blogs_architecture.blogs.Mediator.Post.Commands.Update
{
    public class Validator : PostBaseValidator<CommandModel>
    {       

        public Validator(IReadRepository<Data.Models.Post> postRepository) : base(postRepository)
        {
            RuleFor(query => query).NotEmpty().NotNull();
            RuleFor(x => x.Id).NotEmpty().NotNull().GreaterThan(0).Custom(PostIdValidator);
            RuleFor(x => x).Custom(PostValidator);
        }

        private void PostValidator(CommandModel updatePOSTData, ValidationContext<CommandModel> context)
        {
            if (!string.IsNullOrEmpty(updatePOSTData.Title) && postRepository.Any(new PostFetchSpecification(updatePOSTData.Title, updatePOSTData.Id)))
            {
                context.AddFailure("pOST", "Post already exist!");
            }
        }

        private void PostIdValidator(long id, ValidationContext<CommandModel> context)
        {
            if (!(postRepository.Any(new PostFetchSpecification(id))))
            {
                context.AddFailure("Id", "Id does not exist!");
            }
        }
    }
}
