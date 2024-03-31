namespace dotnet_core_blogs_architecture.Data.Models
{
    public interface IEntityWithTypedId<TId>
    {
        TId Id { get; }
    }
}
