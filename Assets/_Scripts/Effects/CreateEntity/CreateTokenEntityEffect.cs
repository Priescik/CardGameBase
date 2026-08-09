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
        EntityView target = targets.First();
        CardInstance cardInstance = new CardInstance(_token, MatchSetupSystem.Instance.GetPlayerBySide(target.Side));
        CreateEntityGA createEntityGA = new(cardInstance, target);
        return createEntityGA;
    }
}
