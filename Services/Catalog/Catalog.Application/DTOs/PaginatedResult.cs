namespace Catalog.Application.DTOs;

public class PaginatedResult<T>
{
    public int PageIndex { get; }
    public int PageSize { get; }
    public long Count { get; }
    public IEnumerable<T> Data { get; }

    public PaginatedResult(int pageIndex, int pageSize, long count, IEnumerable<T> data)
    {
        PageIndex = pageIndex;
        PageSize = pageSize;
        Count = count;
        Data = data;
    }
}