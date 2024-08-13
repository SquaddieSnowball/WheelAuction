using WheelAuction.Domain.Primitives;

namespace WheelAuction.Application.Abstractions;

public interface IRepository<TEntity> where TEntity : AggregateRoot<TEntity> { }