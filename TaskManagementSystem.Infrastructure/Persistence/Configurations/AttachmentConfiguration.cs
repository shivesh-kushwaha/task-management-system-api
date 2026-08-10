using TaskManagementSystem.Core.Entities;

namespace TaskManagementSystem.Infrastructure.Persistence.Configurations;

internal sealed class AttachmentConfiguration : IEntityTypeConfiguration<Attachment>
{
    public void Configure(EntityTypeBuilder<Attachment> builder)
    {
        builder.ToTable(TableNames.Storage.Attachments);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.FileName)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(x => x.OriginalFileName)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(x => x.FilePath)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(x => x.FileExtension)
            .IsRequired()
            .HasMaxLength(10);

        builder.Property(x => x.ContentType)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.FileSize)
            .IsRequired();

        builder.Property(x => x.Status)
            .IsRequired()
            .HasDefaultValue(RecordStatusEnum.Active);

        builder.Property(x => x.Type)
            .HasConversion<int>()
            .IsRequired();

        builder.Property(x => x.TypeId)
            .IsRequired();

        builder.Property(x => x.StorageType)
            .HasConversion<int>()
            .IsRequired();

        builder.Property(x => x.UploadedById)
            .IsRequired();

        builder.Property(x => x.UploadedAt)
            .IsRequired();

        builder.Property(x => x.DeletedById);

        builder.Property(x => x.DeletedAt);

        // Indexes
        builder.HasIndex(x => new { x.Type, x.TypeId })
            .HasDatabaseName("IX_Attachments_Type_TypeId");
    }
}