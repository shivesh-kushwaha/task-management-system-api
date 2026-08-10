namespace TaskManagementSystem.Core.Dtos.Attachment.AddAttachment;

public sealed record AddAttachmentDto
{
    public string FileName { get; set; } = null!;
    public string OriginalFileName { get; set; } = null!;
    public string FilePath { get; set; } = null!;
    public string FileExtension { get; set; } = null!;
    public string ContentType { get; set; } = null!;
    public long FileSize { get; set; }
    public TypeEnum Type { get; set; }
    public int TypeId { get; set; }
}