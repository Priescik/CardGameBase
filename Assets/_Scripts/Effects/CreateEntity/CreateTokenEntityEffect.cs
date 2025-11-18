using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using System.Linq;
using System;

public class CreateTokenEntityEffect : CreateEntityEffect
{
    /// <summary>
    /// Use for creating entities based on source card
    /// </summary>
    //[SerializeField]
    //[AllowedEntityTargetTypes(EntityTargetType.Empty)]
    //EntityTargetType _allowedTargetType;
    //protected override EntityTargetType AllowedTargetType => _allowedTargetType;

    [SerializeField] private CardTemplate _token;

    public override GameAction GetGameAction(CardInstance cardSource, EntityView entitySource, List<EntityView> targets)
    {
        CardInstance cardInstance = new CardInstance(_token);
        CreateEntityGA createEntityGA = new(cardInstance, targets.FirstOrDefault());
        return createEntityGA;
    }
}
