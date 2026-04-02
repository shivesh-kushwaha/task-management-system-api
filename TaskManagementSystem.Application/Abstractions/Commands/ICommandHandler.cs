using MediatR;

namespace TaskManagementSystem.Application.Abstractions.Commands;

internal interface ICommandHandler<in TCommand>: IRequestHandler<TCommand> where TCommand : ICommand
{
    Task Handle(TCommand command, CancellationToken cancellationToken);
}

internal interface ICommandHandler<in TCommand, TResponse> 
    : IRequestHandler<TCommand, TResponse> where TCommand : ICommand<TResponse>
{
    Task<TResponse> Handle(TCommand command, CancellationToken cancellationToken);
}