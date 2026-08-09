using UnityEngine;

public class isSourceFromMySideC : EffectCondition
{
    public override bool isMet(EntityView triggerOwner, GameAction action)
    {
        if (action is IHasEntitySource actionWithSource &&
            actionWithSource.EntitySource.Side == triggerOwner.Side)
        {
            return !InvertCheck;
        }
        return InvertCheck;
    }
}
