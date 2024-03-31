namespace dotnet_core_blogs_architecture.blogs.Mediator.Post.Commands.Shared
{
    public abstract class PostBaseHandler
    {
        protected void AssignValues(PostBaseCommandModel request, Data.Models.Post postDetail, CancellationToken cancellationToken)
        {
            postDetail.Title = request.Title;
            postDetail.Content = request.Content;
        }
    }
}
