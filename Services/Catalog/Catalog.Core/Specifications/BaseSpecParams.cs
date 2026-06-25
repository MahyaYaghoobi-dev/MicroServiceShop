public class BaseSpecParams
{
    private const int MaxPageSize = 50;
    private int _pageSize = 10;

    public int PageIndex { get; set; } = 1;
    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = value > MaxPageSize ? MaxPageSize : value;
    }
    public string? Search { get; set; }
    public string? SortBy { get; set; }  
    public SortOrder? SortOrder { get; set; }  
}

public enum SortOrder
{
    Asc,
    Desc
}