using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using System.Text.Json;
using TaskManagementSystem.Core.Entities;

namespace TaskManagementSystem.Application.Behaviors;

public sealed class LoggingPipelineBehavior<TRequest, TResponse>(
    ILogger<LoggingPipelineBehavior<TRequest, TResponse>> logger,
    IExceptionLogService exceptionLogService,
    IHttpContextAccessor httpContextAccessor)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;

        logger.LogInformation("Handling {RequestName} {@Request}", requestName, request);

        try
        {
            var response = await next();
            logger.LogInformation("Handled {RequestName} successfully", requestName);
            return response;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error handling {RequestName}", requestName);
            await SaveExceptionLogAsync(ex, request);
            throw new Exception(ex.Message);
        }
    }

    private async Task SaveExceptionLogAsync(Exception ex, TRequest request)
    {
        var context = httpContextAccessor.HttpContext;
        var user = context?.User;

        var exceptionLog = BuildExceptionLog(ex, request, context, user);

        await exceptionLogService.AddAsync(exceptionLog);
    }

    private ExceptionLog BuildExceptionLog(
        Exception ex,
        TRequest request,
        HttpContext? context,
        ClaimsPrincipal? user)
    {
        return new ExceptionLog
        {
            Message = ex.Message,
            StackTrace = ex.StackTrace ?? string.Empty,
            LogType = LogTypeEnum.Error,
            EntityType = InferEntityType(request),
            Description = ex.InnerException?.Message,
            RequestUrl = context?.Request?.Path + context?.Request?.QueryString,
            RequestMethod = context?.Request?.Method,
            IpAddress = GetIpAddress(context),
            AdditionalData = SerializeRequest(request),
            CreatedAt = Utility.GetCurrentDateTimeOffset(),
            CreatedById = GetUserId(user),
            Status = RecordStatusEnum.Active
        };
    }

    // ---------- Helper Methods ----------
    private static int? GetUserId(ClaimsPrincipal? user)
    {
        var idClaim = user?.FindFirst("sub") ?? user?.FindFirst(ClaimTypes.NameIdentifier);
        if (idClaim != null && int.TryParse(idClaim.Value, out var id))
            return id;
        return null;
    }

    private static string? GetIpAddress(HttpContext? context)
    {
        if (context == null) return null;
        var forwarded = context.Request.Headers["X-Forwarded-For"].FirstOrDefault();
        return forwarded?.Split(',').First().Trim() ?? context.Connection.RemoteIpAddress?.ToString();
    }

    private static TypeEnum InferEntityType(TRequest request)
    {
        var typeName = typeof(TRequest).Name;
        if (typeName.Contains("Project")) return TypeEnum.Project;
        if (typeName.Contains("WorkItem")) return TypeEnum.WorkItem;
        if (typeName.Contains("User")) return TypeEnum.User;
        if (typeName.Contains("Team")) return TypeEnum.Team;
        if (typeName.Contains("Comment")) return TypeEnum.Comment;
        return TypeEnum.Other;
    }

    private static int? ExtractEntityId(TRequest request)
    {
        var props = request.GetType().GetProperties();
        var idProp = props.FirstOrDefault(p => p.Name.Equals("Id", StringComparison.OrdinalIgnoreCase) ||
                                               p.Name.Equals("EntityId", StringComparison.OrdinalIgnoreCase));
        if (idProp?.GetValue(request) is int id && id > 0)
            return id;
        return null;
    }

    private static string? SerializeRequest(TRequest request)
    {
        try
        {
            return JsonSerializer.Serialize(request, new JsonSerializerOptions
            {
                WriteIndented = false,
                MaxDepth = 5
            });
        }
        catch
        {
            return null;
        }
    }
}