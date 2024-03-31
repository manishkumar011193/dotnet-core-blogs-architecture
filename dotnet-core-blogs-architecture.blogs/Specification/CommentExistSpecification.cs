using Ardalis.Specification;
using dotnet_core_blogs_architecture.Data.Models;

namespace dotnet_core_blogs_architecture.blogs.Repository.Specifications
{
    public sealed class CommentExistSpecification : Specification<Data.Models.Comment>, ISingleResultSpecification
    {
        public CommentExistSpecification(long postId)
        {
            Query
                .Where(c => c.PostId == postId);
        }

        public CommentExistSpecification(long postId, long currentCommentId)
        {
            Query
                .Where(c => c.PostId == postId && c.Id != currentCommentId);
        }
    }
}
