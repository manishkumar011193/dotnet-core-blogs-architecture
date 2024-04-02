namespace dotnet_core_blogs_architecture.blogs.Mediator.Post.Commands.Shared;
public abstract class PostBaseCommandModel
{
    public string Title { get; set; }

    public string Content { get; set; }
}
