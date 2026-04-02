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
        builder.Property(x => x.ProjectId);
        builder.Property(x => x.ParentId);
        builder.Property(x => x.TypeId);
        builder.Property(x => x.AssignedToId);              // ✅ Removed duplicate & IsRequired() — it's nullable

        // Relationships
        builder.HasOne(x => x.Type)
            .WithMany()
            .HasForeignKey(x => x.TypeId)
            .OnDelete(DeleteBehavior.SetNull);               // ✅ Added explicit delete behavior

        builder.HasOne(x => x.AssignedTo)                   // ✅ Was missing entirely
            .WithMany()
            .HasForeignKey(x => x.AssignedToId)
            .OnDelete(DeleteBehavior.SetNull)
            .IsRequired(false);

        builder.HasMany(x => x.SubTasks)                    // ✅ Was missing entirely
            .WithOne(x => x.Parent)
            .HasForeignKey(x => x.ParentId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired(false);
    }
}