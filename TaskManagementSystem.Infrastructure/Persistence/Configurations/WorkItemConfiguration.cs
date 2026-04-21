// WorkItemConfiguration.cs
using TaskManagementSystem.Core.Entities;
namespace TaskManagementSystem.Infrastructure.Persistence.Configurations;

internal sealed class WorkItemConfiguration : BaseEntityConfiguration<WorkItem>
{
    public override void Configure(EntityTypeBuilder<WorkItem> builder)
    {
        base.Configure(builder);
        builder.ToTable(TableNames.Management.WorkItems);

        builder.Property(x => x.Title).HasMaxLength(100).IsRequired();
        builder.Property(x => x.Description).HasMaxLength(500);
        builder.Property(x => x.DueDate).IsRequired();
        builder.Property(x => x.Priority).IsRequired();
        builder.Property(x => x.TypeId).IsRequired();
        builder.Property(x => x.ProjectId);
        builder.Property(x => x.ParentId);
        builder.Property(x => x.AssignedToId);

        // Relationships
        builder.HasOne(x => x.Type)
            .WithMany()
            .HasForeignKey(x => x.TypeId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(x => x.AssignedTo)
            .WithMany()
            .HasForeignKey(x => x.AssignedToId)
            .OnDelete(DeleteBehavior.SetNull)
            .IsRequired(false);

        builder.HasMany(x => x.SubTasks)
            .WithOne(x => x.Parent)
            .HasForeignKey(x => x.ParentId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired(false);
    }
}