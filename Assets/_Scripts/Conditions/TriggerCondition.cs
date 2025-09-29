using UnityEngine;
using System;

public abstract class TriggerCondition
{
    [SerializeField] protected ReactionTiming ReactionTiming;
    public abstract void SubscribeCondition(Action<GameAction> reaction);
    public abstract void UnsubscribeCondition(Action<GameAction> reaction);
    public abstract bool SubConditionIsMet(EntityView skillOwnerEntity, GameAction gameAction);
}
