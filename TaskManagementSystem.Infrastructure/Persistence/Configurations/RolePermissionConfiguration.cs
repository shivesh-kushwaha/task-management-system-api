namespace TaskManagementSystem.Infrastructure.Persistence.Configurations;

public class RolePermissionConfiguration : BaseEntityConfiguration<RolePermission>
{
    public override void Configure(EntityTypeBuilder<RolePermission> builder)
    {
        base.Configure(builder);

        builder.HasIndex(x => new { x.RoleId, x.PermissionId })
            .IsUnique();
    }
}
