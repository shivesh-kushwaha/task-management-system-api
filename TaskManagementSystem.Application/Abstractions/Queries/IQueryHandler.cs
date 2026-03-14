using MediatR;

namespace TaskManagementSystem.Application.Abstractions.Queries;

internal interface IQueryHandler<in TQuery, TResponse> 
    : IRequestHandler<TQuery, TResponse> where TQuery : IQuery<TResponse>
{
}
