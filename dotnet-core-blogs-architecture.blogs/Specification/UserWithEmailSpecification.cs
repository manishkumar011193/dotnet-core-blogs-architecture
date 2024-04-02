using Ardalis.Specification;

namespace dotnet_core_blogs_architecture.blogs.Specification;
public sealed class UserWithEmailSpecification : Specification<Data.Models.User>, ISingleResultSpecification
{
	public UserWithEmailSpecification(string email)
	{
		Query.Where(b => b.Email.ToLower() == email.ToLower());
	}
}
