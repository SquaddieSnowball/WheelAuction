using WheelAuction.Domain.Primitives;

namespace WheelAuction.Application.Abstractions;

public interface IRepository<T> where T : AggregateRoot { }