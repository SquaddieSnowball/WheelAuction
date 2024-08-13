using MediatR;
using WheelAuction.Domain.Primitives;

namespace WheelAuction.Application.Abstractions.Messaging;

public interface ICommandHandler<TCommand> : IRequestHandler<TCommand, Result>
	where TCommand : ICommand
{ }