// TeamMemberConfiguration.cs  ← was missing, add this
using TaskManagementSystem.Core.Entities;
namespace TaskManagementSystem.Infrastructure.Persistence.Configurations;

internal sealed class TeamMemberConfiguration : BaseEntityConfiguration<TeamMember>
{
    public override void Configure(EntityTypeBuilder<TeamMember> builder)
    {
        base.Configure(builder);
        builder.ToTable(TableNames.Organization.TeamMembers);

        builder.Property(x => x.TeamId).IsRequired();
        builder.Property(x => x.UserId).IsRequired();

        builder.HasOne(x => x.Team)
            .WithMany(x => x.Members)
            .HasForeignKey(x => x.TeamId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.User)
            .WithMany(x => x.TeamMembers)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);   // ✅ Restrict — UserId is non-nullable
    }
}