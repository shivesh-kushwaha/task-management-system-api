using TaskManagementSystem.Core.Entities;
namespace TaskManagementSystem.Infrastructure.Persistence.Configurations;

internal sealed class WorkItemTypeConfiguration : IEntityTypeConfiguration<WorkItemType>
{
    public void Configure(EntityTypeBuilder<WorkItemType> builder)
    {
        builder.ToTable(TableNames.Management.WorkItemTypes);
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name).HasMaxLength(100).IsRequired();
        builder.Property(x => x.CreatedAt).IsRequired();
        builder.Property(x => x.CreatedById).IsRequired();
        builder.Property(x => x.Status).IsRequired();
        builder.Property(x => x.DeletedById);
        builder.Property(x => x.DeletedAt);
    }
}