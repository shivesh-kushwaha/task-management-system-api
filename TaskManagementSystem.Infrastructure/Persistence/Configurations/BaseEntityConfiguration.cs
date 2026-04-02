using TaskManagementSystem.Core.Entities;

namespace TaskManagementSystem.Infrastructure.Persistence.Configurations;

public abstract class BaseEntityConfiguration<T>: IEntityTypeConfiguration<T> where T: BaseEntity
{
    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Status)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.CreatedById);

        builder.Property(x => x.UpdatedAt);

        builder.Property(x => x.UpdatedById);

        builder.Property(x => x.DeletedAt);

        builder.Property(x => x.DeletedById);
    }
}
