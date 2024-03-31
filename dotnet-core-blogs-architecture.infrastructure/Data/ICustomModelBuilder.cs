using Microsoft.EntityFrameworkCore;

namespace dotnet_core_blogs_architecture.Data.Data
{
    public interface ICustomModelBuilder
    {
        void Build(ModelBuilder modelBuilder);
    }
}
