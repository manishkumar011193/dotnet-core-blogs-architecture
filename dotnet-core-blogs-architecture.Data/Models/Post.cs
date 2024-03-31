using dotnet_core_blogs_architecture.Data.Models;

namespace dotnet_core_blogs_architecture.Data.Models
{
    public class Post : EntityBaseWithTypedId<long>
    {
        public string Title { get; set; }

        public string Content { get; set; }
    }
}
