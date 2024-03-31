using AutoMapper;
using dotnet_core_blogs_architecture.blogs.Mediator.User.Shared;
using dotnet_core_blogs_architecture.blogs.Specification;
using dotnet_core_blogs_architecture.Data.Results;
using dotnet_core_blogs_architecture.infrastructure.Interfaces;
using MediatR;

namespace dotnet_core_blogs_architecture.blogs.Mediator.User.Queries.GetById;
public class Handler : IRequestHandler<QueryModel, ValidationResult>
{
	private readonly IReadRepository<Data.Models.User> userReadRepository;
	private readonly IMapper mapper;
	private readonly ILogger logger;

	public Handler(IReadRepository<Data.Models.User> userReadRepository, IMapper mapper, ILogger<Handler> logger)
	{
		this.userReadRepository = userReadRepository;
		this.mapper = mapper;
		this.logger = logger;
	}

	public async Task<ValidationResult> Handle(QueryModel query, CancellationToken cancellationToken)
	{
		logger.LogInformation("Fetching the user with Id: {Id}", query.Id);
		var userDetail = await this.userReadRepository.FirstOrDefaultAsync(new UserWithIdSpecification(query.Id), cancellationToken);
		if (userDetail == null)
		{
			logger.LogError("User with Id: {Id} not found", query.Id);
			return new NotFoundValidationResult("UserId", "Invalid User Id");
		}

		logger.LogInformation("Successfully fetched the user with Id: {Id}", userDetail.Id);
		return new ValidObjectResult(mapper.Map<UserResponseModel>(userDetail));
	}
}
