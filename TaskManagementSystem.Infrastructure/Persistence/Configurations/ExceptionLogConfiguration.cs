using TaskManagementSystem.Core.Entities;

namespace TaskManagementSystem.Infrastructure.Persistence.Configurations;

public sealed class ExceptionLogConfiguration : BaseEntityConfiguration<ExceptionLog>
{
    public override void Configure(EntityTypeBuilder<ExceptionLog> builder)
    {
        base.Configure(builder);

        builder.ToTable(TableNames.Logging.ExceptionLogs);

        builder.Property(x => x.Message)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(x => x.StackTrace)
            .HasColumnType("nvarchar(max)");

        builder.Property(x => x.LogType)
            .HasConversion<int>()
            .IsRequired();

        builder.Property(x => x.EntityType)
            .HasConversion<int>()
            .HasDefaultValue(TypeEnum.Other)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(500);

        builder.Property(x => x.RequestUrl)
            .HasMaxLength(500);

        builder.Property(x => x.RequestMethod)
            .HasMaxLength(50);

        builder.Property(x => x.IpAddress)
            .HasMaxLength(50);

        builder.Property(x => x.AdditionalData)
            .HasColumnType("nvarchar(max)");
    }
}
