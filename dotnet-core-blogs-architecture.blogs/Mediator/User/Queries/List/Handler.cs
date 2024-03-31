using AutoMapper;
using dotnet_core_blogs_architecture.blogs.Mediator.Post.Shared;
using dotnet_core_blogs_architecture.blogs.Mediator.User.Shared;
using dotnet_core_blogs_architecture.blogs.Repository.Specifications;
using dotnet_core_blogs_architecture.Data.Results;
using dotnet_core_blogs_architecture.infrastructure;
using dotnet_core_blogs_architecture.infrastructure.Interfaces;
using MediatR;

namespace dotnet_core_blogs_architecture.blogs.Mediator.User.Queries.List;
public class Handler : IRequestHandler<QueryModel, ValidationResult>
{
	private readonly IReadRepository<Data.Models.User> userRepository;
	private readonly IMapper mapper;
	private readonly ILogger _logger;

	public Handler(IReadRepository<Data.Models.User> userRepository, IMapper mapper, ILogger<Handler> logger)
	{
		this.userRepository = userRepository;
		this.mapper = mapper;
		_logger = logger;
	}
	public async Task<ValidationResult> Handle(QueryModel query, CancellationToken cancellationToken)
	{
		_logger.LogInformation("Fetching the user list");
		var result = await this.userRepository.GetPagninated(new PaginatedUserSpecification(query), query);
		_logger.LogInformation("Successfully fetched the user list");
		return  new ValidObjectResult(mapper.Map<PaginatedResponseModel<UserResponseModel>>(result));
	}

	private List<string> GetSeparatedValues(string filter)
	{
		return filter?.Split("|")?.ToList();
	}
}
