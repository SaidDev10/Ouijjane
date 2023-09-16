namespace Ouijjane.Shared.Domain.Entities;

public abstract class Entity : IEquatable<Entity>
{
    public abstract object?[] GetKeys();

    public bool Equals(Entity? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (IsTransient()) return ReferenceEquals(this, other);
        if (other.GetType() != GetType()) return false;

        if (HasDefaultKeys(this) && HasDefaultKeys(other))
        {
            return false;
        }

        var entity1Keys = GetKeys();
        var entity2Keys = other.GetKeys();

        if (entity1Keys.Length != entity2Keys.Length)
        {
            return false;
        }

        for (var i = 0; i < entity1Keys.Length; i++)
        {
            var entity1Key = entity1Keys[i];
            var entity2Key = entity2Keys[i];

            if (entity1Key == null)
            {
                if (entity2Key == null)
                {
                    continue;
                }

                return false;
            }

            if (entity2Key == null)
            {
                return false;
            }

            if (IsDefaultValue(entity1Key) && IsDefaultValue(entity2Key))
            {
                return false;
            }

            if (!entity1Key.Equals(entity2Key))
            {
                return false;
            }
        }

        return true;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (IsTransient()) return ReferenceEquals(this, obj);
        if (obj.GetType() != GetType()) return false;

        return Equals((Entity)obj);
    }

    public override int GetHashCode()
    {
        return GetHashCodeAggregate(GetKeys(), 17);
    }

    private bool IsTransient()
    {
        return HasDefaultKeys(this);
    }

    public static bool operator ==(Entity? left, Entity? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(Entity? left, Entity? right)
    {
        return !Equals(left, right);
    }

    private static bool HasDefaultKeys(Entity entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        return entity.GetKeys().All(IsDefaultKeyValue);
    }

    private static bool IsDefaultKeyValue(object? value)
    {
        if (value == null)
        {
            return true;
        }

        var type = value.GetType();

        if (type == typeof(int))
        {
            return Convert.ToInt32(value) <= 0;
        }

        if (type == typeof(long))
        {
            return Convert.ToInt64(value) <= 0;
        }

        return IsDefaultValue(value);
    }

    private static bool IsDefaultValue(object? obj)
    {
        return obj == null || obj.Equals(GetDefaultValue(obj.GetType()));
    }

    private static object? GetDefaultValue(Type type)
    {
        return type.IsValueType ? Activator.CreateInstance(type) : null;
    }

    private static int GetHashCodeAggregate<T>(IEnumerable<T> source, int hash)
    {
        unchecked
        {
            hash = source.Aggregate(hash, (current, item) => current * 31 + item!.GetHashCode());
        }

        return hash;
    }
}

public abstract class Entity<TKey> : Entity
{
    public TKey? Id { get; set; }

    public override object?[] GetKeys()
    {
        return new object?[] { Id };
    }
}