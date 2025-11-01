using UnityEngine;
using System.Linq;

public class TargetIsType : TargetCondition
{
    [SerializeField] EntityTargetType _type;
    public override bool isMet(EntityView target)
    {
        if (EntityTargetTypeMapper.IsAssignable(target.GetType(), _type))
        {
            return true; // !InvertCheck;
        }
        return false; // InvertCheck;
    }
}
