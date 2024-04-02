namespace dotnet_core_blogs_architecture.infrastructure;
public abstract class PageQueryModel
{
	public int? Page { get; set; } = 1;
	public int? PageSize { get; set; } = 25;

	public bool? All { get; set; }
	public string Search { get; set; }
	public string SortCol { get; set; }
	public string SortOrder { get; set; }
}
