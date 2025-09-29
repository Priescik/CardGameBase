using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using System.Linq;
using System;

public class CreateEntityEffect : ManualTargetEffect
{
    public override bool IsValidTarget(EntityView target) => target is EmptyEntityView;
    public override Type GetValidType => typeof(EmptyEntityView); // debug purposes

    public override GameAction GetGameAction(CardInstance cardSource, EntityView entitySource, List<EntityView> targets)
    {
        CreateEntityGA createEntityGA = new(cardSource, targets.First());
        return createEntityGA;
    }

}
