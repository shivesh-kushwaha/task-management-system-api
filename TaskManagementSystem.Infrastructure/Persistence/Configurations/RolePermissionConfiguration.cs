using TaskManagementSystem.Core.Entities;

namespace TaskManagementSystem.Infrastructure.Persistence.Configurations;

public sealed class RolePermissionConfiguration : BaseEntityConfiguration<RolePermission>
{
    public override void Configure(EntityTypeBuilder<RolePermission> builder)
    {
        base.Configure(builder);

        builder.ToTable(TableNames.Identity.RolePermissions);

        builder.Property(x => x.RoleId)
            .IsRequired();

        builder.Property(x => x.PermissionId)
            .IsRequired();
    }
}
