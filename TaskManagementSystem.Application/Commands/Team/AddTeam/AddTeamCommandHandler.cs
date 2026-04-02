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
        else if(await teamRepository
            .AsQueryable()
            .AnyAsync(x => x.Status != RecordStatusEnum.Deleted
                && x.Name.Trim().ToUpper().Equals(request.Name.Trim().ToUpper()),
             cancellationToken))
        {
            throw new InvalidOperationException($"Team with name '{request.Name}' already exists.");
        }
        else
        {
            var team = new Core.Entities.Team
            {
                Name = request.Name,
                Description = request.Description,
                CreatedById = request.UserId,
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
