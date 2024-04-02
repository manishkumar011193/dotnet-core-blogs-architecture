using Ardalis.Specification;
using dotnet_core_blogs_architecture.Data;
using dotnet_core_blogs_architecture.Data.Models;
using dotnet_core_blogs_architecture.infrastructure;
using System.Linq.Expressions;

namespace dotnet_core_blogs_architecture.blogs.Repository.Specifications
{
    public class PaginatedPostSpecification : Specification<Post>
    {
        public readonly PageQueryModel PageQueryModel;

        public PaginatedPostSpecification(PageQueryModel pageQueryModel)
        {
            PageQueryModel = pageQueryModel;
            AddQuery();
        }

        private void AddQuery()
        {
            if (!string.IsNullOrWhiteSpace(PageQueryModel.Search))
            {
                Query.Where(post => !string.IsNullOrWhiteSpace(post.Title) && post.Title.ToUpper().Contains(PageQueryModel.Search.ToUpper()));
            }

            if (!string.IsNullOrWhiteSpace(PageQueryModel.SortCol) && !string.IsNullOrWhiteSpace(PageQueryModel.SortOrder))
            {
                switch (PageQueryModel.SortCol.ToLower())
                {
                    case "title":
                        ApplySort(post => post.Title);
                        break;
                    case "content":
                        ApplySort(post => post.Content);
                        break;                   
                }
            }
            else
            {
                Query.OrderByDescending(post => post.Id);
            }
        }

        private void ApplySort(Expression<Func<Post, object>> expression)
        {
            if (PageQueryModel.SortOrder.ToLower().Equals("asc"))
            {
                Query.OrderBy(expression);
            }
            else
            {
                Query.OrderByDescending(expression);
            }
        }
    }
}
