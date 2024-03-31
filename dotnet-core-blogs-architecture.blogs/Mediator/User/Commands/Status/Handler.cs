using AutoMapper;
using DT.Identity.Core.Services.Interface;
using DT.Identity.Core.User.Shared;
using DT.Identity.Repository.Specifications.User;
using DT.Shared.Repository.Interfaces;
using DT.Shared.Web.Results;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace DT.Identity.Core.User.Commands.Status;
public class Handler : IRequestHandler<QueryModel, ValidationResult>
{
	private readonly bool isUserRestricted;
	private readonly IRepository<Repository.Models.User> _userRepository;
	private readonly IAWSCognitoService _aWSCognitoService;
	private readonly IMapper _mapper;
	private readonly ILogger _logger;

	public Handler(IRepository<Repository.Models.User> userRepository, IAWSCognitoService aWSCognitoService, IConfiguration configuration, IMapper mapper, ILogger<Handler> logger)
	{
		this.isUserRestricted = configuration.GetSection("RestrictUserCreation").Get<bool>();

		_userRepository = userRepository;
		_aWSCognitoService = aWSCognitoService;
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

		if (this.isUserRestricted && !userDetail.IsActive && (await _userRepository.CountAsync(new ActiveUserSpecification(), cancellationToken) > 30))
		{
			_logger.LogError("Max User Active Limit Reached for user with Id: {Id}", request.Id);
			return new InvalidValidationResult("Summary", "Max User Active Limit Reached!!");
		}

		bool isSuccess = userDetail.IsActive ? await _aWSCognitoService.DeleteUser(userDetail) : await _aWSCognitoService.AddUser(userDetail);
		if (!isSuccess)
		{
			_logger.LogError("Unable to update the status for the user with Id: {Id}", request.Id);
			return new InvalidValidationResult("Summary", "There is a server error while updating status");
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

	private async Task SetStatus(Repository.Models.User user, CancellationToken cancellationToken)
	{
		user.IsActive = !user.IsActive;
		await _userRepository.UpdateAsync(user, cancellationToken);
	}
}
