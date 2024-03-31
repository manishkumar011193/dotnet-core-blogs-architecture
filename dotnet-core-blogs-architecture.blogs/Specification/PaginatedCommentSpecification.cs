using Ardalis.Specification;
using dotnet_core_blogs_architecture.Data;
using dotnet_core_blogs_architecture.Data.Models;
using dotnet_core_blogs_architecture.infrastructure;
using System.Linq.Expressions;

namespace dotnet_core_blogs_architecture.blogs.Repository.Specifications
{
    public class PaginatedCommentSpecification : Specification<Comment>
    {
        public readonly PageQueryModel PageQueryModel;

        public PaginatedCommentSpecification(PageQueryModel pageQueryModel)
        {
            PageQueryModel = pageQueryModel;
            AddQuery();
        }

        private void AddQuery()
        {
            if (!string.IsNullOrWhiteSpace(PageQueryModel.Search))
            {
                Query.Where(comment => !string.IsNullOrWhiteSpace(comment.Content) && comment.Content.ToUpper().Contains(PageQueryModel.Search.ToUpper()));
            }

            if (!string.IsNullOrWhiteSpace(PageQueryModel.SortCol) && !string.IsNullOrWhiteSpace(PageQueryModel.SortOrder))
            {
                switch (PageQueryModel.SortCol.ToLower())
                {
                    case "content":
                        ApplySort(comment => comment.Content);
                        break;
                    case "postid":
                        ApplySort(comment => comment.PostId);
                        break;                   
                }
            }
            else
            {
                Query.OrderByDescending(comment => comment.PostId);
            }
        }

        private void ApplySort(Expression<Func<Comment, object>> expression)
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
