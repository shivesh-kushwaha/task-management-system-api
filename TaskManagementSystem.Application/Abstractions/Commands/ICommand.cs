using MediatR;

namespace TaskManagementSystem.Application.Abstractions.Commands;

internal interface ICommand : IRequest
{
}

internal interface ICommand<out TResponse> : IRequest<TResponse>
{
}
