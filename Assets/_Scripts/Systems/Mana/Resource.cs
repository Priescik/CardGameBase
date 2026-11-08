using UnityEngine;

public abstract class Resource
{
    public abstract void Spend(ResourceAmount amount);
    public abstract void Gain(ResourceAmount amount, bool refill);
    public abstract bool HasEnough(ResourceAmount amount);
}
/// TODO consider doing:
/// public abstract class Resource {}
/// public abstract class Resource<TAmount> : Resource
///     where TAmount : ResourceAmount {}
/// public class IntegerMana : Resource<IntegerManaAmount> {}
