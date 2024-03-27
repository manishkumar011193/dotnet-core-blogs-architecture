using dotnet_core_blogs_architecture.infrastructure.Models;

namespace dotnet_core_blogs_architecture.infrastructure.Data
{
    public interface IRepository<T> : IRepositoryWithTypedId<T, long> where T : IEntityWithTypedId<long>
    {
    }
}
