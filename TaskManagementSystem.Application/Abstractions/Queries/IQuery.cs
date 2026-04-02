using MediatR;

namespace TaskManagementSystem.Application.Abstractions.Queries;

internal interface IQuery<out TResponse> : IRequest<TResponse>
{
}
