using TaskManagementSystem.Core.Entities;

namespace TaskManagementSystem.Core.Abstractions.Repositories;

public interface IAttachmentRepository
{
    Task AddAsync(Attachment attachment, CancellationToken cancellationToken = default);
    Task AddRangeAsync(List<Attachment> attachments, CancellationToken cancellationToken = default);
    void Update(Attachment attachment);
    void UpdateRange(List<Attachment> attachments);
    Task<Attachment?> FindAsync(int id, CancellationToken cancellationToken = default);
}
