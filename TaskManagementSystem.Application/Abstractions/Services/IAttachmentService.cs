using Microsoft.AspNetCore.Http;

namespace TaskManagementSystem.Application.Abstractions.Services;

public interface IAttachmentService
{
    Task UploadAsync(
        IFormFile file,
        TypeEnum type,
        int entityId,
        int userId,
        CancellationToken cancellationToken = default);
}