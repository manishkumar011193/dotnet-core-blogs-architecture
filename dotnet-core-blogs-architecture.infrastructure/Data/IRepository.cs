using Ardalis.Specification;
namespace dotnet_core_blogs_architecture.infrastructure.Data;

public interface IRepository<T> : IRepositoryBase<T> where T : class
{   
}
