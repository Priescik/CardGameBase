using UnityEngine;
using System.Collections.Generic;

public class DealDamageGA : GameAction, IHasEntitySource, IHasTargets
{
    public int Amount;
    public List<EntityView> Targets {  get; private set; }
    public EntityView EntitySource { get; private set; }
    public DealDamageGA(EntityView source, int amount, List<EntityView> targets)
    {
        Amount = amount;
        Targets = new(targets);
        EntitySource = source;
    }
}
