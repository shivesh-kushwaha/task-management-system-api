using TaskManagementSystem.Application.Commands.Comment.AddComment;
using TaskManagementSystem.Application.Commands.Comment.Dtos;

namespace TaskManagementSystem.Application.Mappings;

internal sealed class CommentMappingProfile: Profile
{
    public CommentMappingProfile()
    {
        CreateMap<AddCommentDto, AddCommentCommand>();
    }
}
