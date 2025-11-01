using UnityEngine;

public class isTheSourceC : EffectCondition
{
    public override bool isMet(EntityView triggerOwner, GameAction action)
    {
        if (action is IHasEntitySource actionWithSource &&
            actionWithSource.EntitySource == triggerOwner)
        {
            return !InvertCheck;
        }
        return InvertCheck;
    }
}
