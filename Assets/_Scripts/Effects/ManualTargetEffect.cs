using UnityEngine;
using System;

public abstract class ManualTargetEffect : Effect
{
    //public abstract Type ExpectedTargetType { get; }
    public abstract bool IsValidTarget(EntityView target);
    public abstract Type GetValidType { get; } // debug purposes
}
