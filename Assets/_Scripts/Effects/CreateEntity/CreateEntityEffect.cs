using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using System.Linq;
using System;

public abstract class CreateEntityEffect : ManualTargetEffect
{
    /// <summary>
    /// Use for creating entities defined in serialization
    /// </summary>
    [SerializeField]
    [AllowedEntityTargetTypes(EntityTargetType.Empty)]
    EntityTargetType _allowedTargetType;
    protected override EntityTargetType AllowedTargetType => _allowedTargetType;

    //public override GameAction GetGameAction(CardInstance cardSource, EntityView entitySource, List<EntityView> targets)
    //{
        //CreateEntityGA createEntityGA = new(cardSource, targets.First());
        //return createEntityGA;
    //}
}
