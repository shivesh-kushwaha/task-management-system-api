using TaskManagementSystem.Application.Commands.Comment.AddComment;
using TaskManagementSystem.Core.Dtos.Comment.AddComment;

namespace TaskManagementSystem.Application.Mappings;

internal sealed class CommentMappingProfile: Profile
{
    public CommentMappingProfile()
    {
        CreateMap<AddCommentDto, AddCommentCommand>();
    }
}
