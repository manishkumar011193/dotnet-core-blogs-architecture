using Ardalis.Specification;
using dotnet_core_blogs_architecture.infrastructure	;

namespace dotnet_core_blogs_architecture.infrastructure.Interfaces;

public interface IReadRepository<T> : IReadRepositoryBase<T> where T : class 
{
	bool Any(ISpecification<T> specification);
	List<T> List(ISpecification<T> specification);
	Task<PaginatedList<T>> GetPagninated(ISpecification<T> specification, PageQueryModel pageQueryModel);
}
