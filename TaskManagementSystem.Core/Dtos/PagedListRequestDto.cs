namespace TaskManagementSystem.Core.Dtos;

public sealed record PagedListRequestDto
{
    public string? FilterKey { get; set; }
    public string Sort { get; set; } = string.Empty;
    public string Order { get; set; } = string.Empty;
    public int PageIndex { get; set; } = 0;
    public int PageSize { get; set; } = 10;

    public string SortExpression()
    {
        return $"{Sort} {Order}";
    }

    public int RecordsToSkip()
    {
        if (PageSize == 0)
        {
            PageSize = 10;
        }

        return PageIndex * PageSize;
    }
}
