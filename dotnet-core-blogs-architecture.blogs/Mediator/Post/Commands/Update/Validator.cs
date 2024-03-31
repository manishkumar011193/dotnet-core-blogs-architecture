using dotnet_core_blogs_architecture.blogs.Specification;
using dotnet_core_blogs_architecture.Data.Data;
using FluentValidation;

namespace dotnet_core_blogs_architecture.blogs.Mediator.Post.Commands.Update
{
    public class Validator : AbstractValidator<CommandModel>
    {
        private readonly IRepository<Data.Models.Post> _postRepository;
        private readonly ILogger<Validator> _logger;

        public Validator(IRepository<Data.Models.Post> postRepository, ILogger<Validator> logger)
        {
            _postRepository = postRepository;
            _logger = logger;

            RuleFor(query => query).NotEmpty().NotNull();
            RuleFor(x => x.Id).NotEmpty().NotNull().GreaterThan(0);
            RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required.");
            RuleFor(x => x.Content).NotEmpty().WithMessage("Content is required.");
            RuleFor(x => x.Id).MustAsync(ValidatePostExistence).WithMessage("Post with specified Id does not exist.");
        }

        private async Task<bool> ValidatePostExistence(long postId, CancellationToken cancellationToken)
        {
            var post = await _postRepository.FirstOrDefaultAsync(new PostFetchSpecification(postId), cancellationToken);
            if (post != null)
            {
                return true;
            }
            return false;

        }
    }
}
