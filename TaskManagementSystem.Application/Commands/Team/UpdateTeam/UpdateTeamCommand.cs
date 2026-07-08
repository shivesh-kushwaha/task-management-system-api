using TaskManagementSystem.Application.Abstractions;

namespace TaskManagementSystem.Application.Commands.Team.UpdateTeam;

public class UpdateTeamCommand : BaseRequest, ICommand
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public ICollection<int> Members { get; set; } = [];
}
