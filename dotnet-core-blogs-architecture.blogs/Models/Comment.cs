namespace dotnet_core_blogs_architecture.blogs.Models
{
    public class Comment
    {
        public long Id { get; set; }

        public long PostId { get; set; }

        public Post Post { get; set; }

        public string Content { get; set; }
    }
}
