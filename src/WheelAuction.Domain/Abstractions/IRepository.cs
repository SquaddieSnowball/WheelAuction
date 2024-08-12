using WheelAuction.Domain.Primitives;

namespace WheelAuction.Domain.Abstractions;

public interface IRepository<T> where T : AggregateRoot { }