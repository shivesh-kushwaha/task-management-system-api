using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using TaskManagementSystem.Core;
using TaskManagementSystem.Core.Entities;
namespace TaskManagementSystem.Application.Services;

internal sealed class AttachmentService : IAttachmentService
{
    private readonly IAttachmentRepository _attachmentRepository;
    private readonly ILogger<AttachmentService> _logger;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public AttachmentService(
        IAttachmentRepository attachmentRepository,
        ILogger<AttachmentService> logger,
        IWebHostEnvironment webHostEnvironment)
    {
        _attachmentRepository = attachmentRepository;
        _logger = logger;
        _webHostEnvironment = webHostEnvironment;
    }

    public async Task UploadAsync(
        IFormFile file,
        TypeEnum type,
        int typeId,
        int userId,
        CancellationToken cancellationToken = default)
    {
        ValidateFile(file);

        var extension = Path.GetExtension(file.FileName);
        var normalizedType = type.ToString().ToLowerInvariant();
        var storedFileName = $"{Guid.NewGuid()}{extension}";
        var relativePath = Path.Combine("uploads", normalizedType, storedFileName).Replace("\\", "/");
        var fullPath = Path.Combine(_webHostEnvironment.WebRootPath, relativePath);

        var directory = Path.GetDirectoryName(fullPath);
        if (!Directory.Exists(directory))
            Directory.CreateDirectory(directory!);

        using (var stream = new FileStream(fullPath, FileMode.Create))
        {
            await file.CopyToAsync(stream, cancellationToken);
        }

        var attachment = new Core.Entities.Attachment
        {
            FileName = file.FileName,
            OriginalFileName = file.FileName,
            FilePath = relativePath,
            FileExtension = extension,
            ContentType = file.ContentType,
            FileSize = file.Length,
            Status = RecordStatusEnum.Active,
            Type = type,
            TypeId = typeId,
            StorageType = StorageTypeEnum.Local,
            UploadedById = userId,
            UploadedAt = DateTimeOffset.UtcNow
        };

        await _attachmentRepository.AddAsync(attachment, cancellationToken);

        _logger.LogInformation("Uploaded {FileName} for {Type} {typeId}",
            file.FileName, type, typeId);
    }

    private void ValidateFile(IFormFile file)
    {
        if (file == null || file.Length == 0)
            throw new ArgumentException("File is empty or null.");

        var maxFileSize = AppSettings.Attachment.MaxFileSize;
        if (file.Length > maxFileSize)
            throw new ArgumentException($"File size exceeds the maximum allowed ({maxFileSize / 1024 / 1024} MB).");

        var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
        if (!AppSettings.Attachment.AllowedExtensions.Contains(extension))
            throw new ArgumentException($"File extension '{extension}' is not allowed.");
    }
}