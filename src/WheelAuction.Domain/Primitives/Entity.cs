namespace WheelAuction.Domain.Primitives;

public abstract class Entity(Guid id) : IEquatable<Entity>
{
	public Guid Id { get; } = id;

	public override string ToString() => $"{nameof(Entity)} {{ {nameof(Id)} = {Id} }}";

	public override int GetHashCode() => Id.GetHashCode();

	public override bool Equals(object? obj) => Equals(obj as Entity);

	public bool Equals(Entity? other) => other?.Id.Equals(Id) is true;

	public static bool operator ==(Entity? first, Entity? second) => first?.Equals(second) is true;

	public static bool operator !=(Entity? first, Entity? second) => first?.Equals(second) is false;
}