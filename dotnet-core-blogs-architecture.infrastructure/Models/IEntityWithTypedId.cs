namespace dotnet_core_blogs_architecture.infrastructure.Models
{
    public interface IEntityWithTypedId<TId>
    {
        TId Id { get; }
    }
}
