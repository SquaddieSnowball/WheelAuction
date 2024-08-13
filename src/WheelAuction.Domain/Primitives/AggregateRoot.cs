namespace WheelAuction.Domain.Primitives;

public abstract class AggregateRoot<TEntity>(Guid id = default) : Entity<TEntity>(id) { }