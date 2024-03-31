using AutoMapper;
using dotnet_core_blogs_architecture.blogs.Mediator.Post.Shared;
using dotnet_core_blogs_architecture.blogs.Mediator.User.Shared;
using dotnet_core_blogs_architecture.blogs.Specification;
using dotnet_core_blogs_architecture.Data.Data;
using dotnet_core_blogs_architecture.Data.Results;
using dotnet_core_blogs_architecture.infrastructure.Data;
using MediatR;

namespace dotnet_core_blogs_architecture.blogs.Mediator.User.Commands.Status;
public class Handler : IRequestHandler<QueryModel, ValidationResult>
{
	private readonly bool isUserRestricted;
	private readonly IRepository<Data.Models.User> _userRepository;	
	private readonly IMapper _mapper;
	private readonly ILogger _logger;

	public Handler(IRepository<Data.Models.User> userRepository, IConfiguration configuration, IMapper mapper, ILogger<Handler> logger)
	{
		this.isUserRestricted = configuration.GetSection("RestrictUserCreation").Get<bool>();

		_userRepository = userRepository;		
		_mapper = mapper;
		_logger = logger;
	}
	public async Task<ValidationResult> Handle(QueryModel request, CancellationToken cancellationToken)
	{
		_logger.LogInformation("Fetching the user with Id: {Id}", request.Id);
		var userDetail = await this._userRepository.FirstOrDefaultAsync(new UserWithIdSpecification(request.Id), cancellationToken);
		if (userDetail == null)
		{
			_logger.LogError("User not found with Id: {Id}", request.Id);
			return new NotFoundValidationResult("UserId", "Invalid User Id");
		}		
		await SetStatus(userDetail, cancellationToken);
		var saved = await _userRepository.SaveChangesAsync(cancellationToken);
		if (saved <= 0)
		{
			_logger.LogError("Unable to update the status for the user with Id: {Id}", request.Id);
			return new InvalidValidationResult("Summary", "There was a server error updating user status.");
		}

		return new ValidObjectResult(_mapper.Map<UserResponseModel>(userDetail));
	}

	private async Task SetStatus(Data.Models.User user, CancellationToken cancellationToken)
	{
		user.IsActive = !user.IsActive;
		await _userRepository.UpdateAsync(user, cancellationToken);
	}
}
