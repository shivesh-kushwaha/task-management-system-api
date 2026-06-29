using TaskManagementSystem.Application.Abstractions;
using TaskManagementSystem.Core.Dtos.Permission.GetPermissionListByUserId;

namespace TaskManagementSystem.Application.Queries.Permission.GetPermitListByUserId;

public class GetPermissionListByUserIdQuery: BaseCommand, IQuery<List<GetPermissionListByUserIdDto>>
{
}
