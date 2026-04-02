using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using TaskManagementSystem.Application.Commands;

namespace TaskManagementSystem.Application.Behaviors;

public sealed class UserContextPipelineBehavior<TRequest, TResponse>(
    IHttpContextAccessor httpContextAccessor)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var userId = httpContextAccessor.HttpContext?.User?
            .FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (request is BaseCommand baseCommand &&
            int.TryParse(userId, out var parsedUserId))
        {
            baseCommand.UserId = parsedUserId;
        }

        return await next();
    }
}