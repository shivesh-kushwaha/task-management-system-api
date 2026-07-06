using TaskManagementSystem.Application.Abstractions;

namespace TaskManagementSystem.Application.Commands.Team.AddTeam;

public sealed class AddTeamCommand : BaseRequest, ICommand
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public ICollection<int> Members { get; set; } = [];
}
