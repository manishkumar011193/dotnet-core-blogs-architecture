using dotnet_core_blogs_architecture.blogs.Mediator.Post.Shared;

namespace dotnet_core_blogs_architecture.blogs.Mediator.Comment.Shared;
public class CommentResponseModel
{
    public int Id { get; set; }

    public PostResponseModel Post { get; set; }

    public string Content { get; set; }
}

