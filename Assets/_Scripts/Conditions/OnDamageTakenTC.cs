using UnityEngine;
using System;

public class OnDamageTakenTC : TriggerCondition
{
    public override void SubscribeCondition(Action<GameAction> reaction)
    {
        ActionSystem.SubscribeReaction<DealDamageGA>(reaction, ReactionTiming);
    }
    public override void UnsubscribeCondition(Action<GameAction> reaction)
    {
        ActionSystem.UnsubscribeReaction<DealDamageGA>(reaction, ReactionTiming);
    }
    public override bool SubConditionIsMet(EntityView skillOwnerEntity, GameAction gameAction)
    {
        // if target is this
        if (gameAction is DealDamageGA dealDamageGA 
            && dealDamageGA.Targets.Contains(skillOwnerEntity))
        {
            return true;
        }
        return false;
        
    }
}