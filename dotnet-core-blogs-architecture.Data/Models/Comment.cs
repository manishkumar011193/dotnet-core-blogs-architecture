using dotnet_core_blogs_architecture.Data.Models;
using dotnet_core_blogs_architecture.infrastructure.Models;

namespace dotnet_core_blogs_architecture.Data.Models
{
    public class Comment : EntityBaseWithTypedId<long>
    {

        public long PostId { get; set; }

        public Post Post { get; set; }

        public string Content { get; set; }
    }
}
