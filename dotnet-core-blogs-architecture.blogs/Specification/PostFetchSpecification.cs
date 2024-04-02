using Ardalis.Specification;

namespace dotnet_core_blogs_architecture.blogs.Specification
{
    public sealed class PostFetchSpecification : Specification<Data.Models.Post>, ISingleResultSpecification
    {
        public PostFetchSpecification(long id)
        {
            Query
                .Where(b => b.Id == id);      
        }
        public PostFetchSpecification(string post)
        {
            Query
            .Where(b => b.Title.ToLower() == post.ToLower());
        }
        public PostFetchSpecification(string Title, long id)
        {
            Query
            .Where(b => b.Title.ToLower() == Title.ToLower() && b.Id != id);
        }
    }
}
