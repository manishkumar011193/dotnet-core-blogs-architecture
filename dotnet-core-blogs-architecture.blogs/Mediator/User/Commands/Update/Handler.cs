using AutoMapper;
using DT.Identity.Core.User.Commands.Shared;
using DT.Identity.Core.User.Shared;
using DT.Identity.Repository.Specifications.User;
using DT.Shared.Repository.Interfaces;
using DT.Shared.Web.Results;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DT.Identity.Core.User.Commands.Update;
public class Handler : UserBaseHandler, IRequestHandler<CommandModel, ValidationResult>
{
	private readonly IRepository<Repository.Models.User> _userRepository;
	private readonly IMapper _mapper;
	private readonly ILogger _logger;

	public Handler(IRepository<Repository.Models.User> userRepository, IMapper mapper, ILogger<Handler> logger) : base()
	{
		_userRepository = userRepository;
		_mapper = mapper;
		_logger = logger;
	}

	public async Task<ValidationResult> Handle(CommandModel request, CancellationToken cancellationToken)
	{
		_logger.LogInformation("Fetching the user with Id: {Id}", request.Id);
		var userDetail = await this._userRepository.FirstOrDefaultAsync(new UserWithIdSpecification(request.Id), cancellationToken);
		if (userDetail == null)
		{
			_logger.LogError("User with Id: {Id} not found", request.Id);
			return new NotFoundValidationResult("UserId", "Invalid User Id");
		}

		_logger.LogInformation("Found the user with Id: {Id}", userDetail.Id);
		AssignValues(request, userDetail, cancellationToken);
		await _userRepository.UpdateAsync(userDetail, cancellationToken);

		var saved = await _userRepository.SaveChangesAsync(cancellationToken);
		if (saved <= 0)
		{
			_logger.LogError("Unable to save the user with Id: {Id}", userDetail.Id);
			return new InvalidValidationResult("Summary", "There was a server error updating 'User'");
		}

		userDetail = await this._userRepository.FirstOrDefaultAsync(new UserWithIdSpecification(request.Id), cancellationToken);
		_logger.LogInformation("Successfully updated the user with Id: {Id}", userDetail.Id);
		return new ValidObjectResult(_mapper.Map<UserResponseModel>(userDetail));
	}
}
