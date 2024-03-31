using AutoMapper;
using DT.Identity.Core.User.Shared;
using DT.Identity.Repository.Models;
using DT.Identity.Repository.Specifications.User;
using DT.Shared.Repository.Interfaces;
using DT.Shared.Web.Results;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DT.Identity.Core.User.Queries.GetById;
public class Handler : IRequestHandler<QueryModel, ValidationResult>
{
	private readonly IReadRepository<Repository.Models.User> userReadRepository;
	private readonly IMapper mapper;
	private readonly ILogger logger;

	public Handler(IReadRepository<Repository.Models.User> userReadRepository, IMapper mapper, ILogger<Handler> logger)
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
