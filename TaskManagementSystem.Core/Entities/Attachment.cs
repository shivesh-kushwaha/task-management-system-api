namespace TaskManagementSystem.Core.Entities;

public sealed class Attachment
{
    public int Id { get; set; }
    public string FileName { get; set; } = null!;
    public string OriginalFileName { get; set; } = null!;
    public string FilePath { get; set; } = null!;
    public string FileExtension { get; set; } = null!;
    public string ContentType { get; set; } = null!;
    public long FileSize { get; set; }
    public RecordStatusEnum Status { get; set; }
    public TypeEnum Type { get; set; }
    public int TypeId { get; set; }
    public StorageTypeEnum StorageType { get; set; }
    public int UploadedById { get; set; }
    public DateTimeOffset UploadedAt { get; set; }
    public int? DeletedById { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }
}