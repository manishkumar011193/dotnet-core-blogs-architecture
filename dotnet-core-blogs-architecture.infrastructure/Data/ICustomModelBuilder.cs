using Microsoft.EntityFrameworkCore;

namespace dotnet_core_blogs_architecture.infrastructure.Data
{
    public interface ICustomModelBuilder
    {
        void Build(ModelBuilder modelBuilder);
    }
}
