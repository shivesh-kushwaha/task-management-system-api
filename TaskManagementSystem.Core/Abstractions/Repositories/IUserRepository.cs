using TaskManagementSystem.Core.Dtos;
using TaskManagementSystem.Core.Dtos.User.GetUserById;
using TaskManagementSystem.Core.Dtos.User.GetUserPagedList;
using TaskManagementSystem.Core.Dtos.User.GetWorkItemListById;
using TaskManagementSystem.Core.Entities;

namespace TaskManagementSystem.Core.Abstractions.Repositories;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetUserByEmailAsync(string email);
    Task<PagedListResponseDto<GetUserPagedListDto>> GetPagedListAsync(PagedListRequestDto request, int userId, CancellationToken cancellationToken = default);
    Task<List<GetWorkItemListByIdDto>> GetWorkItemListByIdAsync(int id, CancellationToken cancellationToken);
    Task<GetUserByIdDto?> GetUserByIdAsync(int id, CancellationToken cancellationToken = default);
}
