using TaskManagementSystem.Core.Entities;

namespace TaskManagementSystem.Infrastructure.Persistence.Configurations;

public sealed class PermissionConfiguration: BaseEntityConfiguration<Permission>
{
    public override void Configure(EntityTypeBuilder<Permission> builder)
    {
        base.Configure(builder);

        builder.ToTable(TableNames.Identity.Permissions);

        builder.Property(x => x.Name)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.Code)
            .HasMaxLength(50)
            .IsRequired();

        builder.HasIndex(x => x.Code)
            .IsUnique();
    }
}
