using UnityEngine;
using System;
using SerializeReferenceEditor;

public abstract class TriggerCondition
{
    [SerializeField] protected ReactionTiming ReactionTiming;
    [field: SerializeReference, SR] protected EffectCondition[] SubConditions;
    public abstract void SubscribeCondition(Action<GameAction> reaction);
    public abstract void UnsubscribeCondition(Action<GameAction> reaction);
    public bool SubConditionIsMet(EntityView skillOwnerEntity, GameAction gameAction)
    {
        foreach (var condition in SubConditions)
        {
            if (!condition.isMet(skillOwnerEntity, gameAction)) return false;
        }
        return true;
    }
}
