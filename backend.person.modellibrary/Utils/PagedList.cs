namespace backend.person.modellibrary.Utils;

public class PagedList<T>
{
    public List<T> Items { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
    public bool HasNextPage => Page * PageSize < TotalCount;
    public bool HasPreviousPage => PageSize > 1;

    public static PagedList<T> Create(IQueryable<T> query, int page = 0, int pageSize = 0)
    {
        var totalCount = query.Count();
        List<T> items;
        if (page != 0 && pageSize != 0)
            items = query.Skip(((page) - 1) * pageSize).Take(pageSize).ToList();
        else items = query.ToList();
        
        return new PagedList<T>
        {
            Items = items,
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount
        };
    }

}