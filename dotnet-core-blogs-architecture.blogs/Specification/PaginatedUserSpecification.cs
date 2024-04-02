using Ardalis.Specification;
using dotnet_core_blogs_architecture.infrastructure;
using System.Linq.Expressions;

namespace dotnet_core_blogs_architecture.blogs.Repository.Specifications;

public sealed class PaginatedUserSpecification : Specification<Data.Models.User>
{
	public readonly PageQueryModel PageQueryModel;	

	public PaginatedUserSpecification(PageQueryModel pageQueryModel)
	{
		this.PageQueryModel = pageQueryModel;		
		AddQuery();
	}

	private void AddQuery()
	{		

		if (!string.IsNullOrWhiteSpace(PageQueryModel.SortCol) && !string.IsNullOrWhiteSpace(PageQueryModel.SortOrder))
		{
			switch (PageQueryModel.SortCol.ToLower())
			{
				case "name":
					ApplySort(x => x.FirstName);
					break;				
				case "status":
					ApplySort(x => x.IsActive);
					break;
			}
		}
		else
		{
			Query.OrderBy(x => x.FirstName);
		}
	}

	private void ApplySort(Expression<Func<Data.Models.User, object>> expression)
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

	private Expression<Func<Data.Models.User, bool>> BuildOrSearchExpression(List<Expression<Func<Data.Models.User, bool>>> expressions)
	{
		if (expressions.Count == 1)
		{
			return expressions[0];
		}

		var orExpression = expressions.Skip(2).Aggregate(
			Expression.OrElse(expressions[0].Body, Expression.Invoke(expressions[1], expressions[0].Parameters[0])),
			(x, y) => Expression.OrElse(x, Expression.Invoke(y, expressions[0].Parameters[0])));

		return Expression.Lambda<Func<Data.Models.User, bool>>(orExpression, expressions[0].Parameters);
	}
}