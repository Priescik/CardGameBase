using UnityEngine;
using System.Collections.Generic;

public class SideRandomEntityTM : TargetMode
{
    public override List<EntityView> GetTargets()
    {
        throw new System.NotImplementedException();
        //EntityView target = EntitySystem.Instance.Enemies[Random.Range(0, Ene)]
        //return new(EntitySystem.Instance.All);
    }
}

