namespace TaskManagementSystem.Application.Commands.Role.Dtos;

public sealed record AddRoleDto
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
}
