using UnityEngine;
using SerializeReferenceEditor;

[System.Serializable]
public abstract class EffectCondition
{
    /// <summary>
    /// Turns condition to "is Not..."
    /// </summary>
    [SerializeField] [Tooltip("Turns consition to \"is Not...\"")] protected bool InvertCheck = false;

    /// <param name="triggerOwner">Passed by TriggerCondition - this is the owner of the trigger</param>
    /// <param name="action">Passed by TriggerCondition - this is the GameAction causing the trigger</param>
    public abstract bool isMet(EntityView triggerOwner, GameAction action);
}