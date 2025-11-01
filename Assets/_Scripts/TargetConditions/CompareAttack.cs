using UnityEngine;
using System.Linq;

[System.Serializable]
public class CompareAttack : TargetCondition
{
    //[SerializeField] ComparisonSign _sign;
    [SerializeField] int _value;
    public override bool isMet(EntityView target)
    {
        //return target.
        return false;
    }
}
