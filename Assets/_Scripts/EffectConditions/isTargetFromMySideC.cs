using UnityEngine;
using System.Linq;

public class isTargetFromMySideC : EffectCondition
{
    public override bool isMet(EntityView triggerOwner, GameAction action)
    {
        if (action is IHasTargets actionWithTarget &&
            actionWithTarget.Targets.Any(t => t.Side == triggerOwner.Side))
        {
            return !InvertCheck;
        }
        return InvertCheck;
    }
}
