using TaskManagementSystem.Core.Dtos;
using TaskManagementSystem.Core.Dtos.ExceptionLog.GetExceptionLogById;
using TaskManagementSystem.Core.Dtos.ExceptionLog.GetExceptionLogPagedList;
using TaskManagementSystem.Core.Entities;

namespace TaskManagementSystem.Infrastructure.Persistence.Repositories;

internal sealed class ExceptionLogRepository(ApplicationDbContext dbContext)
    : Repository<ExceptionLog>(dbContext), IExceptionLogRepository
{
    public async Task<PagedListResponseDto<GetExceptionLogPagedListDto>> GetPagedListAsync(
        GetExceptionLogPagedListRequestDto request, CancellationToken cancellationToken = default)
    {
        var sortExpression = request.SortExpression();
        var recordToSkip = request.RecordsToSkip();

        // 1. Base query with left joins
        var query = from e in dbContext.ExceptionLogs.AsNoTracking()
                    join createdBy in dbContext.Users.AsNoTracking()
                        on e.CreatedById equals createdBy.Id into createdGroup
                    from createdUser in createdGroup.DefaultIfEmpty()
                    join updatedBy in dbContext.Users.AsNoTracking()
                        on e.UpdatedById equals updatedBy.Id into updatedGroup
                    from updatedUser in updatedGroup.DefaultIfEmpty()
                    where e.Status != RecordStatusEnum.Deleted
                          && (createdUser == null || createdUser.Status != RecordStatusEnum.Deleted)
                          && (updatedUser == null || updatedUser.Status != RecordStatusEnum.Deleted)
                    select new { e, createdUser, updatedUser };

        // 2. Apply search filter
        if (!string.IsNullOrWhiteSpace(request.FilterKey))
        {
            var filterKey = request.FilterKey.Trim().ToUpper();
            query = query.Where(x =>
                EF.Functions.Like(x.e.Message.Trim().ToUpper(), $"%{filterKey}%") ||
                EF.Functions.Like(x.e.StackTrace.Trim().ToUpper(), $"%{filterKey}%") ||
                (x.e.RequestUrl != null && EF.Functions.Like(x.e.RequestUrl.Trim().ToUpper(), $"%{filterKey}%")) ||
                (x.e.Description != null && EF.Functions.Like(x.e.Description.Trim().ToUpper(), $"%{filterKey}%")) ||
                (x.createdUser != null && EF.Functions.Like((x.createdUser.FirstName.Trim() + " " + x.createdUser.LastName.Trim()).ToUpper(), $"%{filterKey}%"))
            );
        }

        // 3. Apply additional filters (all nullable)
        if (request.LogType.HasValue)
            query = query.Where(x => x.e.LogType == request.LogType.Value);

        if (request.EntityType.HasValue)
            query = query.Where(x => x.e.EntityType == request.EntityType.Value);

        if (request.FromDate.HasValue)
            query = query.Where(x => x.e.CreatedAt >= request.FromDate.Value);

        if (request.ToDate.HasValue)
            query = query.Where(x => x.e.CreatedAt <= request.ToDate.Value);

        // 4. Project to DTO
        var response = query.Select(x => new GetExceptionLogPagedListDto
        {
            Id = x.e.Id,
            Message = x.e.Message,
            StackTrace = x.e.StackTrace,
            LogType = x.e.LogType,
            EntityType = x.e.EntityType,
            Description = x.e.Description,
            RequestUrl = x.e.RequestUrl,
            RequestMethod = x.e.RequestMethod,
            IpAddress = x.e.IpAddress,
            AdditionalData = x.e.AdditionalData,
            CreatedAt = x.e.CreatedAt,
            Status = x.e.Status,
            CreatedByFirstName = x.createdUser != null ? x.createdUser.FirstName : string.Empty,
            CreatedByLastName = x.createdUser != null ? x.createdUser.LastName : string.Empty,
            UpdatedByFirstName = x.updatedUser != null ? x.updatedUser.FirstName : string.Empty,
            UpdatedByLastName = x.updatedUser != null ? x.updatedUser.LastName : string.Empty,
            // Full names (concatenated)
            CreatedByFullName = x.createdUser != null
                ? x.createdUser.FirstName + " " + x.createdUser.LastName
                : string.Empty,
            UpdatedByFullName = x.updatedUser != null
                ? x.updatedUser.FirstName + " " + x.updatedUser.LastName
                : string.Empty
        });

        // 5. Return paginated result
        return new PagedListResponseDto<GetExceptionLogPagedListDto>
        {
            TotalCount = await response.CountAsync(cancellationToken),
            Items = await response
                        .OrderBy(sortExpression)
                        .Skip(recordToSkip)
                        .Take(request.PageSize)
                        .ToListAsync(cancellationToken)
        };
    }

    public Task<GetExceptionLogByIdDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return (from e in dbContext.ExceptionLogs.AsNoTracking()
                join createdBy in dbContext.Users.AsNoTracking()
                    on e.CreatedById equals createdBy.Id into createdGroup
                from createdUser in createdGroup.DefaultIfEmpty()
                join updatedBy in dbContext.Users.AsNoTracking()
                    on e.UpdatedById equals updatedBy.Id into updatedGroup
                from updatedUser in updatedGroup.DefaultIfEmpty()
                where e.Id == id
                      && e.Status != RecordStatusEnum.Deleted
                      && (createdUser == null || createdUser.Status != RecordStatusEnum.Deleted)
                      && (updatedUser == null || updatedUser.Status != RecordStatusEnum.Deleted)
                select new GetExceptionLogByIdDto
                {
                    Id = e.Id,
                    Message = e.Message,
                    StackTrace = e.StackTrace,
                    LogType = e.LogType,
                    EntityType = e.EntityType,
                    Description = e.Description,
                    RequestUrl = e.RequestUrl,
                    RequestMethod = e.RequestMethod,
                    IpAddress = e.IpAddress,
                    AdditionalData = e.AdditionalData,
                    CreatedAt = e.CreatedAt,
                    Status = e.Status,
                    CreatedByFirstName = createdUser != null ? createdUser.FirstName : string.Empty,
                    CreatedByLastName = createdUser != null ? createdUser.LastName : string.Empty,
                    UpdatedByFirstName = updatedUser != null ? updatedUser.FirstName : string.Empty,
                    UpdatedByLastName = updatedUser != null ? updatedUser.LastName : string.Empty,
                    CreatedByFullName = createdUser != null
                        ? createdUser.FirstName + " " + createdUser.LastName
                        : string.Empty,
                    UpdatedByFullName = updatedUser != null
                        ? updatedUser.FirstName + " " + updatedUser.LastName
                        : string.Empty
                }).SingleOrDefaultAsync(cancellationToken);
    }
}