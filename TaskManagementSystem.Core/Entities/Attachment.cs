namespace TaskManagementSystem.Core.Entities;

public class Attachment: BaseEntity
{
    public CommentTypeEnum Type { get; set; }
    public int TypeId { get; set; }
    public string FileName { get; set; } = null!;
    public string OriginalFileName { get; set; } = null!;
    public string FilePath { get; set; } = null!;
    public string FileExtension { get; set; } = null!;
    public int FileSize { get; set; }
    public string ContentType { get; set; } = null!;
}
