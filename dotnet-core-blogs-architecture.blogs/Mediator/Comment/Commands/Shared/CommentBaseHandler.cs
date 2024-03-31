namespace dotnet_core_blogs_architecture.blogs.Mediator.Comment.Commands.Shared
{
    public abstract class CommentBaseHandler
    {
        protected void AssignValues(CommentBaseCommandModel request, Data.Models.Comment postDetail, CancellationToken cancellationToken)
        {
            postDetail.PostId = request.PostId;
            postDetail.Content = request.Content;
        }
    }
}
