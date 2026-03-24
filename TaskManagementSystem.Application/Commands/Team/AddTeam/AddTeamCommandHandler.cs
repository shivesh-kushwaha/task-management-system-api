namespace TaskManagementSystem.Application.Commands.Team.AddTeam;

internal sealed class AddTeamCommandHandler(
    ITeamRepository teamRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<AddTeamCommand>
{
    public async Task Handle(AddTeamCommand request, CancellationToken cancellationToken)
    {
        if (!request.Members.Any())
        {
            throw new InvalidOperationException("Please select members.");
        }
        else
        {
            var team = new Core.Entities.Team
            {
                Name = request.Name,
                Description = request.Description,
                CreatedById = null,
                CreatedAt = Utility.GetCurrentDateTimeOffset(),
                Status = RecordStatusEnum.Active,
                Members = [.. request.Members.Select(id => new Core.Entities.TeamMember
                {
                    UserId = id,
                    CreatedById = null,
                    CreatedAt = Utility.GetCurrentDateTimeOffset(),
                    Status = RecordStatusEnum.Active
                })]
            };

            await teamRepository.AddAsync(team);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
