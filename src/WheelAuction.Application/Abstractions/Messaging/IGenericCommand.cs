using MediatR;
using WheelAuction.Domain.Primitives;

namespace WheelAuction.Application.Abstractions.Messaging;

public interface ICommand<TResponse> : ICommandBase, IRequest<Result<TResponse>> { }