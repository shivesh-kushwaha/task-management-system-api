namespace TaskManagementSystem.Core.Dtos;

public record SelectListItemDto
{
    public int Key { get; set; }
    public string Value { get; set; } = null!;
}
