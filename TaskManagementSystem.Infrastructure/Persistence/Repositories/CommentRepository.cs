using TaskManagementSystem.Core.Entities;

namespace TaskManagementSystem.Infrastructure.Persistence.Repositories;

internal sealed class CommentRepository(ApplicationDbContext dbContext)
    : Repository<Comment>(dbContext), ICommentRepository
{
}
