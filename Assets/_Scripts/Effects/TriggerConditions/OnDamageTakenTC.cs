using UnityEngine;
using System;

public class OnDamageTakenTC : TriggerCondition
{
    public override void SubscribeCondition(Action<GameAction> reaction)
    {
        Debug.LogWarning("OnDamageTaken is depreciated, use OnDamageDealt with subcondition ''isTarget'' instead");
        ActionSystem.SubscribeReaction<DealDamageGA>(reaction, ReactionTiming);
    }
    public override void UnsubscribeCondition(Action<GameAction> reaction)
    {
        ActionSystem.UnsubscribeReaction<DealDamageGA>(reaction, ReactionTiming);
    }
}