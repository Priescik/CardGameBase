using UnityEngine;
using System;

public class OnDestroyedTC : TriggerCondition
{
    public override void SubscribeCondition(Action<GameAction> reaction)
    {
        ActionSystem.SubscribeReaction<DestroyEntityGA>(reaction, ReactionTiming);
    }
    public override void UnsubscribeCondition(Action<GameAction> reaction)
    {
        ActionSystem.UnsubscribeReaction<DestroyEntityGA>(reaction, ReactionTiming);
    }
    //public override bool SubConditionIsMet(EntityView skillOwnerEntity, GameAction gameAction)
    //{
    //    // if target is this
    //    if (gameAction is DestroyEntityGA destroyEntityGA 
    //        && destroyEntityGA.Targets.Contains(skillOwnerEntity))
    //    {
    //        return true;
    //    }
    //    return false;
    //}
}
