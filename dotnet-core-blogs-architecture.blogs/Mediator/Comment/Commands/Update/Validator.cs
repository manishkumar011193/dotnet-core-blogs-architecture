using dotnet_core_blogs_architecture.blogs.Repository.Specifications;
using dotnet_core_blogs_architecture.blogs.Specification;
using dotnet_core_blogs_architecture.Data.Data;
using FluentValidation;

namespace dotnet_core_blogs_architecture.blogs.Mediator.Comment.Commands.Update
{
    public class Validator : AbstractValidator<CommandModel>
    {
        private readonly IRepository<Data.Models.Comment> _commentRepository;

        public Validator(IRepository<Data.Models.Comment> commentRepository)
        {
            _commentRepository = commentRepository;

            RuleFor(x => x).NotEmpty().NotNull();
            RuleFor(x => x.Id).NotEmpty().NotNull().GreaterThan(0);
            RuleFor(x => x.Content).NotEmpty().WithMessage("Content is required.");
            RuleFor(x => x.Id).MustAsync(ValidateCommentExistence).WithMessage("Comment with specified Id does not exist.");
        }

        private async Task<bool> ValidateCommentExistence(long commentId, CancellationToken cancellationToken)
        {
            var comment = await _commentRepository.FirstOrDefaultAsync(new CommentFetchSpecification(commentId), cancellationToken);
            return comment != null;
        }
    }
}
