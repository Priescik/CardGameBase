using UnityEngine;
using System.Collections.Generic;

public class AllEntitiesTM : TargetMode
{
    public override List<EntityView> GetTargets()
    {
        return new(EntitySystem.Instance.All);
    }
}
