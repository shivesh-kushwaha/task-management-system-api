using TaskManagementSystem.Application.Commands.Comment.AddComment;
using TaskManagementSystem.Application.Commands.Comment.UpdateComment;
using TaskManagementSystem.Application.Queries.Comment.GetCommentPagedList;
using TaskManagementSystem.Core.Dtos.Comment.AddComment;
using TaskManagementSystem.Core.Dtos.Comment.GetCommentPagedList;
using TaskManagementSystem.Core.Dtos.Comment.UpdateComment;

namespace TaskManagementSystem.Application.Mappings;

internal sealed class CommentMappingProfile : Profile
{
    public CommentMappingProfile()
    {
        // Command
        CreateMap<AddCommentDto, AddCommentCommand>();
        CreateMap<UpdateCommentDto, UpdateCommentCommand>();

        // Query
        CreateMap<GetCommentPagedListRequestDto, GetCommentPagedListQuery>()
            .ForMember(src => src.Request, opt => opt.MapFrom(dest => dest));
    }
}
