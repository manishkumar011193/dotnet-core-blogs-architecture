namespace dotnet_core_blogs_architecture.blogs.Mediator.User.Commands.Shared;
public abstract class UserBaseHandler
{
    protected UserBaseHandler()
    {
    }

	protected void AssignValues(UserBaseCommandModel request, Data.Models.User userDetail, CancellationToken cancellationToken)
	{
		userDetail.FirstName = request.FirstName;
		userDetail.LastName = request.LastName;
		userDetail.MiddleName = request.MiddleName;
		userDetail.Email = request.Email;
		userDetail.Mobile = request.Mobile;
		userDetail.IsActive = request.IsActive;
						
	}	
}
