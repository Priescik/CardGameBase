using UnityEngine;
using System;
using System.Linq;

public abstract class ManualTargetEffect : Effect
{
    protected abstract EntityTargetType AllowedTargetType { get; } // field cannot be declared in abstract, property can

    public bool IsValidTarget(EntityView target)
    {
        return EntityTargetTypeMapper.IsAssignable(target.GetType(), AllowedTargetType);
    }

    /// <summary>
    /// Just for debug purposes!
    /// </summary>
    public string GetValidType { get => AllowedTargetType.ToString(); }
}
