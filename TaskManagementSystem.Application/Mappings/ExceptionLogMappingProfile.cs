using TaskManagementSystem.Application.Commands.ExceptionLog.UpdateExceptionLog;
using TaskManagementSystem.Application.Queries.ExceptionLog.GetExceptionLogPagedList;
using TaskManagementSystem.Core.Dtos.ExceptionLog.GetExceptionLogPagedList;
using TaskManagementSystem.Core.Dtos.ExceptionLog.UpdateExceptionLog;

namespace TaskManagementSystem.Application.Mappings;

internal sealed class ExceptionLogMappingProfile : Profile
{
    public ExceptionLogMappingProfile()
    {
        // Commands
        CreateMap<UpdateExceptionLogDto, UpdateExceptionLogCommand>();

        // Queries
        CreateMap<GetExceptionLogPagedListRequestDto, GetExceptionLogPagedListQuery>()
            .ForMember(src => src.Filter, opt => opt.MapFrom(dest => dest));
    }
}
