namespace dotnet_core_blogs_architecture.blogs.Mediator.Comment.Commands.Shared;

public abstract class CommentBaseCommandModel
{
    public long PostId { get; set; }

    public string Content { get; set; }
}
