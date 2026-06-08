using TaskManagementSystem.Core.Entities;

namespace TaskManagementSystem.Application.Commands.Team.UpdateTeam;

internal sealed class UpdateTeamCommandHandler(
    ITeamRepository teamRepository,
    ITeamMemberRepository teamMemberRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateTeamCommand>
{
    public async Task Handle(UpdateTeamCommand request, CancellationToken cancellationToken)
    {
        if (await teamRepository.AsQueryable()
            .AnyAsync(x => x.Status != RecordStatusEnum.Deleted
                && x.Id != request.Id
                && x.Name.Trim().ToUpper().Equals(request.Name.Trim().ToUpper()),
                cancellationToken))
        {
            throw new InvalidOperationException($"Team with '{request.Name}' already exists.");
        }

        var team = await teamRepository.GetByIdAsync(request.Id, cancellationToken);

        if (team == null)
        {
            throw new InvalidOperationException("Team not found.");
        }
        else
        {
            team.Name = request.Name;
            team.Description = request.Description;
            team.UpdatedAt = Utility.GetCurrentDateTimeOffset();
            team.UpdatedById = request.UserId;

            var userIdsToDelete = team.Members.Select(t => t.UserId).Except(request.Members.Select(x => x));
            var userIdsToUpdate = team.Members.Select(t => t.UserId).Intersect(request.Members.Select(x => x)); // No use for now
            var userIdsToAdd = request.Members.Except(team.Members.Select(x => x.UserId));

            if (userIdsToDelete.Any())
            {
                var members = team.Members
                    .Where(m => userIdsToDelete.Contains(m.UserId));

                foreach (var member in members)
                {
                    member.TeamId = team.Id;
                    member.UpdatedById = request.UserId;
                    member.UpdatedAt = Utility.GetCurrentDateTimeOffset();
                    member.Status = RecordStatusEnum.Deleted;
                }
            }

            if (userIdsToAdd.Any())
            {
                var members = userIdsToAdd.Select(x => new TeamMember
                {
                    TeamId = team.Id,
                    UserId = x,
                    CreatedById = request.UserId,
                    CreatedAt = Utility.GetCurrentDateTimeOffset(),
                    Status = RecordStatusEnum.Active
                }).ToList();

                await teamMemberRepository.AddRangeAsync(members);
            }

            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
