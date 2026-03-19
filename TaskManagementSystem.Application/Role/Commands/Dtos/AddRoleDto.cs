namespace TaskManagementSystem.Application.Role.Commands.Dtos;

public sealed record AddRoleDto
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
}
