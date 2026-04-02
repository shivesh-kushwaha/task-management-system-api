using TaskManagementSystem.Core.Entities;

namespace TaskManagementSystem.Infrastructure.Persistence.Configurations;

internal sealed class CommentConfiguration : BaseEntityConfiguration<Comment>
{
    public override void Configure(EntityTypeBuilder<Comment> builder)
    {
        base.Configure(builder);

        builder.ToTable(TableNames.Management.Comments);

        builder.Property(x => x.Description)
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(x => x.Type)
            .IsRequired();

        builder.Property(x => x.TypeId)
            .IsRequired();
    }
}
