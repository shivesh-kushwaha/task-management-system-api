namespace TaskManagementSystem.Core.Dtos;

public sealed record PagedListResponseDto<T>
{
    public List<T> Items { get; set; } = [];
    public int TotalCount { get; set; }
}
