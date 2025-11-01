using System;
using System.Linq;


public static class EntityTargetTypeMapper
{
    public static Type[] GetMappedTypes(EntityTargetType type) => type switch
    {
        EntityTargetType.Empty => new[] { typeof(EmptyEntityView) },
        EntityTargetType.Minion => new[] { typeof(MinionEntityView) },
        EntityTargetType.Player => new[] { typeof(PlayerEntityView) },
        EntityTargetType.MinionOrPlayer => new[] { typeof(PlayerEntityView), typeof(MinionEntityView) },
        _ => Array.Empty<Type>()
    };

    public static bool IsAssignable(Type subject, EntityTargetType expected)
    {
        return GetMappedTypes(expected).Any(t => subject.IsAssignableFrom(t)); // maybe change to contains?
    }

    public static bool IsAssignable(Type subject, Type expected)
    {
        return subject.IsAssignableFrom(expected);
    }
}