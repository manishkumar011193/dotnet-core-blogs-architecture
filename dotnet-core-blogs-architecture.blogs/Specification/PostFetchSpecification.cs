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
    }
}
