using TaskManagementSystem.Application.Abstractions;
using TaskManagementSystem.Core.Dtos.Permission.GetPermissionListByUserId;

namespace TaskManagementSystem.Application.Queries.Permission.GetPermitListByUserId;

public class GetPermissionListByUserIdQuery: BaseRequest, IQuery<List<GetPermissionListByUserIdDto>>
{
}
