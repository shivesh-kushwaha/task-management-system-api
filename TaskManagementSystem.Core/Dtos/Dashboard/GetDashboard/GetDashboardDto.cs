namespace TaskManagementSystem.Core.Dtos.Dashboard.GetDashboard;

public sealed record GetDashboardDto
{
    public int TotalProject { get; set; }
    public int TotalTask { get; set; }
    public int TotalTeam { get; set; }
    public int TotalUser { get; set; }
    public List<SelectListItemDto> Tasks { get; set; } = [];
}
