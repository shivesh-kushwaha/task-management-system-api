using TaskManagementSystem.Core.Entities;

namespace TaskManagementSystem.Infrastructure.Persistence.Configurations;

public sealed class PermissionGroupConfiguration: BaseEntityConfiguration<PermissionGroup>
{
    public override void Configure(EntityTypeBuilder<PermissionGroup> builder)

    {
        base.Configure(builder);

        builder.ToTable(TableNames.Identity.PermissionGroups);

        builder.Property(x => x.Name)
            .HasMaxLength(100)
            .IsRequired();
    }
}
