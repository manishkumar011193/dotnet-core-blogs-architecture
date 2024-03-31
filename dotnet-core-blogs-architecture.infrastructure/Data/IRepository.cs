using Ardalis.Specification;
namespace dotnet_core_blogs_architecture.Data.Data;

public interface IRepository<T> : IRepositoryBase<T> where T : class
{   
}
