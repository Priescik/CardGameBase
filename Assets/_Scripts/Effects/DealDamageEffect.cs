using UnityEngine;
using System;
using System.Collections.Generic;

public class DealDamageEffect : ManualTargetEffect
{
    [SerializeField] int _amount;
    public override bool IsValidTarget(EntityView target) => target is MinionEntityView;
    public override Type GetValidType => typeof(MinionEntityView); // debug purposes

    public override GameAction GetGameAction(CardInstance cardSource, EntityView entitySource, List<EntityView> targets)
    {
        DealDamageGA dealDamageGA = new(entitySource, _amount, targets);
        return dealDamageGA;
    }
}
