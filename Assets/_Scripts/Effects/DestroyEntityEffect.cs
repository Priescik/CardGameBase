using UnityEngine;
using System;
using System.Collections.Generic;

public class DestroyEntityEffect : ManualTargetEffect
{
    
    public override bool IsValidTarget(EntityView target) => target is MinionEntityView; // TODO generalize
    public override Type GetValidType => typeof(MinionEntityView); // debug purposes

    public override GameAction GetGameAction(CardInstance cardSource, EntityView entitySource, List<EntityView> targets)
    {
        DestroyEntityGA destroyEntityGA = new(TODO, targets);
        return destroyEntityGA;
    }
}
