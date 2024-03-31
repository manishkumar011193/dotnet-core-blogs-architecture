using AutoMapper;
using DT.Identity.Core.Common;
using DT.Identity.Core.User.Shared;
using DT.Identity.Repository.Specifications.User;
using DT.Shared.Repository.Interfaces;
using DT.Shared.Web.Results;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DT.Identity.Core.User.Queries.List;
public class Handler : IRequestHandler<QueryModel, ValidationResult>
{
	private readonly IReadRepository<Repository.Models.User> userRepository;
	private readonly IMapper mapper;
	private readonly ILogger _logger;

	public Handler(IReadRepository<Repository.Models.User> userRepository, IMapper mapper, ILogger<Handler> logger)
	{
		this.userRepository = userRepository;
		this.mapper = mapper;
		_logger = logger;
	}
	public async Task<ValidationResult> Handle(QueryModel query, CancellationToken cancellationToken)
	{
		_logger.LogInformation("Fetching the user list");
		var result = await this.userRepository.GetPagninated(new PaginatedUserSpecification(query, GetSeparatedValues(query.LocFilter), GetSeparatedValues(query.DepFilter), GetSeparatedValues(query.DesFilter), GetSeparatedValues(query.TypeFilter)), query);
		_logger.LogInformation("Successfully fetched the user list");
		return  new ValidObjectResult(mapper.Map<PaginatedResponseModel<UserResponseModel>>(result));
	}

	private List<string> GetSeparatedValues(string filter)
	{
		return filter?.Split("|")?.ToList();
	}
}
