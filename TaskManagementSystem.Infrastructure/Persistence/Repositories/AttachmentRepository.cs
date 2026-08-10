using TaskManagementSystem.Core.Entities;

namespace TaskManagementSystem.Infrastructure.Persistence.Repositories;

internal sealed class AttachmentRepository(ApplicationDbContext dbContext) : IAttachmentRepository
{
    public async Task AddAsync(Attachment attachment, CancellationToken cancellationToken = default)
    {
        await dbContext.Attachments.AddAsync(attachment, cancellationToken);
    }

    public async Task AddRangeAsync(List<Attachment> attachments, CancellationToken cancellationToken = default)
    {
        await dbContext.Attachments.AddRangeAsync(attachments, cancellationToken);
    }

    public void Update(Attachment attachment)
    {
        dbContext.Attachments.Update(attachment);
    }

    public void UpdateRange(List<Attachment> attachments)
    {
        dbContext.Attachments.UpdateRange(attachments);
    }

    public async Task<Attachment?> FindAsync(int id, CancellationToken cancellationToken = default)
    {
        return await dbContext.Attachments.FindAsync(id, cancellationToken);
    }
}
