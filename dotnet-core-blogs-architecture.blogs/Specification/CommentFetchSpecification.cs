using Ardalis.Specification;
using dotnet_core_blogs_architecture.Data.Models;
using System.Collections.Generic;

namespace dotnet_core_blogs_architecture.blogs.Repository.Specifications
{
    public sealed class CommentFetchSpecification : Specification<Comment>
    {
        public CommentFetchSpecification(long commentId)
        {
            Query.Where(c => c.Id == commentId);
        }

        public CommentFetchSpecification(List<long> commentIds)
        {
            Query.Where(c => commentIds.Contains(c.Id));
        }
    }
}
