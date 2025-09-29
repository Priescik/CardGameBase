using UnityEngine;
using System.Collections.Generic;

public class AllSideEntitiesTM : TargetMode
{
    public override List<EntityView> GetTargets()
    {
        return new(EntitySystem.Instance.All); //TODO side
    }
}
