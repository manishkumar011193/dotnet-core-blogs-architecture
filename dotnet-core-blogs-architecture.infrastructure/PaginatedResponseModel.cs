namespace dotnet_core_blogs_architecture.infrastructure;
public class PaginatedResponseModel<T>
{
	public int Page { get; set; }
	public int Total { get; set; }
	public int TotalPages { get; set; }
	public List<T> Data { get; set; }
}