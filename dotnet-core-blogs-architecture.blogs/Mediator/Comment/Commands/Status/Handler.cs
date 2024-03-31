//using AutoMapper;
//using dotnet_core_blogs_architecture.blogs.Mediator.Comment.Shared;
//using dotnet_core_blogs_architecture.blogs.Specification;
//using dotnet_core_blogs_architecture.Data.Data;
//using dotnet_core_blogs_architecture.Data.Results;
//using MediatR;

//namespace dotnet_core_blogs_architecture.blogs.Mediator.Comment.Commands.Status
//{
//    public class Handler : IRequestHandler<QueryModel, ValidationResult>
//    {
//        private readonly IRepository<Data.Models.Comment> _commentRepository;
//        private readonly IMapper _mapper;
//        private readonly ILogger<Handler> _logger;

//        public Handler(IRepository<Data.Models.Comment> commentRepository, IMapper mapper, ILogger<Handler> logger)
//        {
//            _commentRepository = commentRepository;
//            _mapper = mapper;
//            _logger = logger;
//        }

//        public async Task<ValidationResult> Handle(QueryModel request, CancellationToken cancellationToken)
//        {
//            _logger.LogInformation("Fetching the comment with Id: {Id}", request.Id);

//            // Fetch the comment from the repository
//            var commentDetail = await _commentRepository.FirstOrDefaultAsync(new CommentFetchSpecification(request.Id), cancellationToken);

//            if (commentDetail == null)
//            {
//                _logger.LogError("Comment not found with Id: {Id}", request.Id);
//                return new NotFoundValidationResult("CommentId", "Invalid Comment Id");
//            }

//            // Update the status of the comment (example: toggle the status)
//            // Modify the status property of the comment entity as needed
//            // For example, you might have a property like IsActive in the Comment model
//            // Here, we'll just invert the current status
//            commentDetail.IsActive = !commentDetail.IsActive;

//            // Update the comment in the repository
//            await _commentRepository.UpdateAsync(commentDetail, cancellationToken);
//            var saved = await _commentRepository.SaveChangesAsync(cancellationToken);

//            if (saved <= 0)
//            {
//                _logger.LogError("Unable to update the status for comment with Id: {Id}", request.Id);
//                return new InvalidValidationResult("Summary", "There was a server error updating comment status.");
//            }

//            // Return the updated comment as a valid result
//            return new ValidObjectResult(_mapper.Map<CommentResponseModel>(commentDetail));
//        }
//    }
//}
