namespace TaskManagementSystem.Infrastructure.Persistence.Configurations;

public sealed class UserRoleConfiguration: BaseEntityConfiguration<UserRole>
{
    public override void Configure(EntityTypeBuilder<UserRole> builder)
    {
        base.Configure(builder);

        builder.ToTable(TableNames.Identity.UserRoles);

        builder.HasIndex(x => new { x.UserId, x.RoleId })
            .IsUnique();
    }
}
