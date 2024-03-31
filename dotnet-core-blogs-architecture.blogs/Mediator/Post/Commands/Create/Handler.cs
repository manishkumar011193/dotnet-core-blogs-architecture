using AutoMapper;
using dotnet_core_blogs_architecture.Data.Results;
using dotnet_core_blogs_architecture.infrastructure.Data;
using MediatR;

namespace dotnet_core_blogs_architecture.blogs.Mediator.Post.Commands.Create;
public class Handler : Shared.PostBaseHandler, IRequestHandler<CommandModel, ValidationResult>
{
    private readonly IMapper _mapper;
    private readonly IRepository<Data.Models.Post> _postRepository;
    private readonly ILogger _logger;
    public Handler(IMapper mapper, IRepository<Data.Models.Post> postRepository, ILogger<Handler> logger) : base()
    {
        _mapper = mapper;
        _postRepository = postRepository;
        _logger = logger;
    }

    public async Task<ValidationResult> Handle(CommandModel request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Request initiated for Post creation with Title : {Post Title}", request.Title);

        // Create a new post entity
        var postDetail = new Data.Models.Post
        {
            // Assign properties from the request
            Title = request.Title,
            Content = request.Content,
        };

        // Save the post entity
        await _postRepository.AddAsync(postDetail, cancellationToken);
        await _postRepository.SaveChangesAsync(cancellationToken);

        // Map the saved post to a response model and return it
        return new CreatedResult(_mapper.Map<Post.Shared.PostResponseModel>(postDetail));
    }


}
