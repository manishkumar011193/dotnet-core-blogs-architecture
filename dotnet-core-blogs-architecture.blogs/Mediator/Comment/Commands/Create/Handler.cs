using AutoMapper;
using dotnet_core_blogs_architecture.blogs.Mediator.Comment.Shared;
using dotnet_core_blogs_architecture.blogs.Repository.Specifications;
using dotnet_core_blogs_architecture.blogs.Specification;
using dotnet_core_blogs_architecture.Data.Data;
using dotnet_core_blogs_architecture.Data.Results;
using dotnet_core_blogs_architecture.infrastructure.Data;
using MediatR;

namespace dotnet_core_blogs_architecture.blogs.Mediator.Comment.Commands.Create
{
    public class Handler : IRequestHandler<CommandModel, ValidationResult>
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Data.Models.Comment> _commentRepository;
        private readonly ILogger _logger;

        public Handler(IMapper mapper, IRepository<Data.Models.Comment> commentRepository, ILogger<Handler> logger)
        {
            _mapper = mapper;
            _commentRepository = commentRepository;
            _logger = logger;
        }

        public async Task<ValidationResult> Handle(CommandModel request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Request initiated for comment creation with content: {Content}", request.Content);

            var commentDetail = new Data.Models.Comment();

            // Assign properties from the request
            commentDetail.Content = request.Content;
            commentDetail.PostId = request.PostId; // Assuming PostId is provided in the request

            // Add the comment entity
            await _commentRepository.AddAsync(commentDetail, cancellationToken);
            await _commentRepository.SaveChangesAsync(cancellationToken);

            // Retrieve the saved comment entity to include related data
            commentDetail = await _commentRepository.FirstOrDefaultAsync(new CommentFetchSpecification(commentDetail.Id), cancellationToken);

            _logger.LogInformation("Successfully created the comment with Id: {Id}", commentDetail.Id);

            // Map the saved comment to a response model and return it
            return new CreatedResult(_mapper.Map<CommentResponseModel>(commentDetail));
        }
    }
}
