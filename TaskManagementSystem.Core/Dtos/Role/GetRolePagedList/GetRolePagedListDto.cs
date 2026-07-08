namespace TaskManagementSystem.Core.Dtos.Role.GetRolePagedList;

public sealed record GetRolePagedListDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Code { get; set; } = null!;
    public string? Description { get; set; }
}
