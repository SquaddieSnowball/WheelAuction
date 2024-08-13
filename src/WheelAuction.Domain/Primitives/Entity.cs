namespace WheelAuction.Domain.Primitives;

public abstract class Entity<TEntity>(Guid id = default) : IEquatable<Entity<TEntity>>
{
	public Guid Id { get; } = (id == default) ? Guid.NewGuid() : id;

	public override string ToString() => $"{nameof(Entity<TEntity>)} {{ {nameof(Id)} = {Id} }}";

	public override int GetHashCode() => Id.GetHashCode();

	public override bool Equals(object? obj) => Equals(obj as Entity<TEntity>);

	public bool Equals(Entity<TEntity>? other) => other?.Id.Equals(Id) is true;

	public abstract Result<TEntity> Validate();

	public static bool operator ==(Entity<TEntity>? first, Entity<TEntity>? second) => first?.Equals(second) is true;

	public static bool operator !=(Entity<TEntity>? first, Entity<TEntity>? second) => first?.Equals(second) is false;
}