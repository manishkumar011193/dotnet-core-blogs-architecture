namespace DT.Shared.Repository;
public class PaginatedList<T> : List<T>
{
	public int Page { get; private set; }
	public int TotalPages { get; private set; }
	public int Total { get; private set; }

	public PaginatedList(List<T> items, int count, int page, int pageSize)
	{
		Page = page;
		Total = count;
		TotalPages = (int)Math.Ceiling(count / (double)pageSize);

		AddRange(items);
	}
}
