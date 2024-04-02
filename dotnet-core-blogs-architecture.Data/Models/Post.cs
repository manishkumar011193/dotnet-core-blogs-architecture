using dotnet_core_blogs_architecture.Data.Models;
using dotnet_core_blogs_architecture.infrastructure.Models;

namespace dotnet_core_blogs_architecture.Data.Models
{
    public class Post : EntityBaseWithTypedId<long>
    {
        public string Title { get; set; }

        public string Content { get; set; }
    }
}
