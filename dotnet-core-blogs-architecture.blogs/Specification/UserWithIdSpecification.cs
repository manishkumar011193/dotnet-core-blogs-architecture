using Ardalis.Specification;

namespace dotnet_core_blogs_architecture.blogs.Specification;

public sealed class UserWithIdSpecification : Specification<Data.Models.User>, ISingleResultSpecification
{
	public UserWithIdSpecification(long userId)
	{
		Query		
			.Where(b => b.Id == userId);
	}
}