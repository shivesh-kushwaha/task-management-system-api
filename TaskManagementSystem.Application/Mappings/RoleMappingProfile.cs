using TaskManagementSystem.Application.Commands.Role.UpsertRole;
using TaskManagementSystem.Application.Queries.Role.GetRolePagedList;
using TaskManagementSystem.Core.Dtos.Role.UpsertRole;

namespace TaskManagementSystem.Application.Mappings;

internal sealed class RoleMappingProfile : Profile
{
    public RoleMappingProfile()
    {
        // Commands
        CreateMap<UpsertRoleDto, UpsertRoleCommand>();

        // Queries
        CreateMap<PagedListRequestDto, GetRolePagedListQuery>()
            .ForMember(src => src.Filter, opt => opt.MapFrom(dest => dest));
    }
}
