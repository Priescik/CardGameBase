using UnityEngine;

public class isTheTargetC : EffectCondition
{
    public override bool isMet(EntityView triggerOwner, GameAction action)
    {
        if (action is IHasTargets actionWithTarget &&
            actionWithTarget.Targets.Contains(triggerOwner))
        {
            return !InvertCheck;
        }
        return InvertCheck;
    }
}
