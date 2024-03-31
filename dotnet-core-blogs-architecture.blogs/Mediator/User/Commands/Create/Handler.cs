using AutoMapper;
using dotnet_core_blogs_architecture.blogs.Mediator.Post.Shared;
using dotnet_core_blogs_architecture.blogs.Mediator.User.Commands.Shared;
using dotnet_core_blogs_architecture.blogs.Mediator.User.Shared;
using dotnet_core_blogs_architecture.blogs.Specification;
using dotnet_core_blogs_architecture.Data.Data;
using dotnet_core_blogs_architecture.Data.Results;
using dotnet_core_blogs_architecture.infrastructure.Data;
using MediatR;

namespace dotnet_core_blogs_architecture.blogs.Mediator.User.Commands.Create;

public class Handler : UserBaseHandler, IRequestHandler<CommandModel, ValidationResult>
{
	private readonly IRepository<Data.Models.User> _userRepository;
	private readonly IMapper _mapper;
	private readonly ILogger _logger;
	public Handler(IRepository<Data.Models.User> userRepository, IMapper mapper, ILogger<Handler> logger) : base()
	{
		_userRepository = userRepository;
		_mapper = mapper;
		_logger = logger;
	}

	public async Task<ValidationResult> Handle(CommandModel request, CancellationToken cancellationToken)
	{
		_logger.LogInformation("Request initiated for user creation with Email: {Email}", request.Email);
		var userDetail = new Data.Models.User()
		{
			Email = request.Email,
			IsActive = false			
		};
		AssignValues(request, userDetail, cancellationToken);
		await _userRepository.AddAsync(userDetail, cancellationToken);

		var saved = await _userRepository.SaveChangesAsync(cancellationToken);
		if (saved <= 0)
		{
			_logger.LogError("Unable to save the user with Id: {Id}", userDetail.Id);
			return new InvalidValidationResult("Summary", "There was a server error saving 'User'");
		}

		userDetail = await this._userRepository.FirstOrDefaultAsync(new UserWithIdSpecification(userDetail.Id), cancellationToken);
		_logger.LogInformation("Successfully created the user with Id: {Id}", userDetail.Id);
		return new CreatedResult(_mapper.Map<UserResponseModel>(userDetail));
	}
}
