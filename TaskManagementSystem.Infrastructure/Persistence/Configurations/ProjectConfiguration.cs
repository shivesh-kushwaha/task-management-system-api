// ProjectConfiguration.cs
using TaskManagementSystem.Core.Entities;
namespace TaskManagementSystem.Infrastructure.Persistence.Configurations;

internal sealed class ProjectConfiguration : BaseEntityConfiguration<Project>
{
    public override void Configure(EntityTypeBuilder<Project> builder)
    {
        base.Configure(builder);
        builder.ToTable(TableNames.Management.Projects);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Description)
            .HasMaxLength(500);

        builder.Property(x => x.Type)
            .IsRequired();

        builder.Property(x => x.TeamId);

        // Relationships
        builder.HasOne(x => x.Team)              // ✅ Use typed nav property, not anonymous HasOne<Team>()
            .WithMany(x => x.Projects)
            .HasForeignKey(x => x.TeamId)
            .OnDelete(DeleteBehavior.SetNull)
            .IsRequired(false);

        builder.HasMany(x => x.WorkItems)
            .WithOne(x => x.Project)             // ✅ Reference back-nav on WorkItem
            .HasForeignKey(x => x.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}