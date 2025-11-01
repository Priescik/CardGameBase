using System;
using UnityEngine;

[AttributeUsage(AttributeTargets.Field)]
public class AllowedEntityTargetTypesAttribute : PropertyAttribute
{
    public EntityTargetType[] AllowedValues { get; }

    public AllowedEntityTargetTypesAttribute(params EntityTargetType[] allowedValues)
    {
        AllowedValues = allowedValues;
    }
}
