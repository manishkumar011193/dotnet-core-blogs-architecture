using FluentValidation;
using dotnet_core_blogs_architecture.blogs.Repository.Specifications;
using dotnet_core_blogs_architecture.infrastructure.Data;

namespace dotnet_core_blogs_architecture.blogs.Mediator.Comment.Commands.Create
{
    public class Validator : AbstractValidator<CommandModel>
    {
        private readonly IRepository<Data.Models.Comment> _commentRepository;

        public Validator(IRepository<Data.Models.Comment> commentRepository)
        {
            _commentRepository = commentRepository;

            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("Content is required.");

            RuleFor(x => x.PostId)
                .NotEmpty().WithMessage("PostId is required.")
                .GreaterThan(0).WithMessage("PostId must be greater than 0.");

            RuleFor(x => x.PostId)
                .MustAsync(ValidatePostExistence).WithMessage("Post with specified Id does not exist.");
        }

        private async Task<bool> ValidatePostExistence(long postId, CancellationToken cancellationToken)
        {
            var post = await _commentRepository.FirstOrDefaultAsync(new CommentExistSpecification(postId), cancellationToken);
            return post != null;
        }
    }
}
