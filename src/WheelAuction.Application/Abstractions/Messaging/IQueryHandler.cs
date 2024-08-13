using MediatR;
using WheelAuction.Domain.Primitives;

namespace WheelAuction.Application.Abstractions.Messaging;

public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
	where TQuery : IQuery<TResponse>
{ }