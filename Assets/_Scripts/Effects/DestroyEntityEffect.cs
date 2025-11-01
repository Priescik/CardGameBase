using UnityEngine;
using System;
using System.Collections.Generic;

public class DestroyEntityEffect : ManualTargetEffect
{
    [SerializeField]
    [AllowedEntityTargetTypes(EntityTargetType.Minion)]
    EntityTargetType _allowedTargetType;
    protected override EntityTargetType AllowedTargetType => _allowedTargetType;

    public override GameAction GetGameAction(CardInstance cardSource, EntityView entitySource, List<EntityView> targets)
    {
        DestroyEntityGA destroyEntityGA = new(targets);
        return destroyEntityGA;
    }
}
