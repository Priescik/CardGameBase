using UnityEngine;
using SerializeReferenceEditor;

[System.Serializable]
public abstract class TargetCondition
{    
    //[SerializeField][Tooltip("Turns consition to \"is Not...\"")] protected bool InvertCheck = false;
    public abstract bool isMet(EntityView target);
}