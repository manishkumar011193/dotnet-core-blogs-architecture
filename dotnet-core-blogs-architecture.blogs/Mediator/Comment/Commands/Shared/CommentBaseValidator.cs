using dotnet_core_blogs_architecture.blogs.Mediator.Comment.Shared;
using dotnet_core_blogs_architecture.blogs.Repository.Specifications;
using FluentValidation;
using DT.Shared.Repository.Interfaces;
using dotnet_core_blogs_architecture.blogs.Specification;

namespace dotnet_core_blogs_architecture.blogs.Mediator.Comment.Commands.Shared
{
    public abstract class CommentBaseValidator<T> : AbstractValidator<T> where T : CommentBaseCommandModel
    {
        protected readonly IReadRepository<Data.Models.Comment> _commentRepository;
        protected readonly IReadRepository<Data.Models.Post> _postRepository;

        public CommentBaseValidator(IReadRepository<Data.Models.Comment> commentRepository, IReadRepository<Data.Models.Post> postRepository)
        {
            _commentRepository = commentRepository;
            _postRepository = postRepository;

            RuleFor(x => x).NotNull();
            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("Comment content cannot be empty.")
                .MaximumLength(500).WithMessage("Comment content cannot exceed 500 characters.");
            RuleFor(x => x.PostId)
                .NotEmpty().WithMessage("PostId cannot be empty.")
                .GreaterThan(0).WithMessage("PostId must be greater than 0.")
                .MustAsync(ValidatePostExistence).WithMessage("The specified post does not exist.");
        }

        private async Task<bool> ValidatePostExistence(long postId, CancellationToken cancellationToken)
        {
            var post = await _postRepository.FirstOrDefaultAsync(new PostFetchSpecification(postId), cancellationToken);
            return post != null;
        }
    }
}
