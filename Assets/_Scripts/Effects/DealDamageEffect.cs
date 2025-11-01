using UnityEngine;
using System;
using System.Collections.Generic;

public class DealDamageEffect : ManualTargetEffect
{
    [SerializeField]
    [AllowedEntityTargetTypes(EntityTargetType.Minion, EntityTargetType.MinionOrPlayer, EntityTargetType.Player)]
    EntityTargetType _allowedTargetType;
    protected override EntityTargetType AllowedTargetType => _allowedTargetType;

    [SerializeField] int _amount;

    public override GameAction GetGameAction(CardInstance cardSource, EntityView entitySource, List<EntityView> targets)
    {
        DealDamageGA dealDamageGA = new(entitySource, _amount, targets);
        return dealDamageGA;
    }
}
