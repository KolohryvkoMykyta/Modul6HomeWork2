namespace Catalog.Host.Models.Requests;

public class PaginatedItemsRequest<T>
    where T : struct, Enum
{
    public int PageIndex { get; set; }

    public int PageSize { get; set; }

    public Dictionary<T, int>? Filters { get; set; }
}