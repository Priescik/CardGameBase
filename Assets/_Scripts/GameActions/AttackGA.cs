using UnityEngine;
using System.Collections.Generic;

public class AttackGA : GameAction, IHasEntitySource, IHasTargets
{
    public List<EntityView> Targets {  get; private set; }
    public EntityView EntitySource { get; private set; }
    public AttackGA(EntityView source, EntityView target)
    {
        EntitySource = source;
        Targets = new(){ target };
    }
}
