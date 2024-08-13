using MediatR;
using WheelAuction.Domain.Primitives;

namespace WheelAuction.Application.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>> { }