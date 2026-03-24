namespace TaskManagementSystem.Application.Commands.Team.AddTeam;

public sealed class AddTeamCommand : BaseCommand, ICommand
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public ICollection<int> Members { get; set; } = [];
}
