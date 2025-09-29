using UnityEngine;
using System.Collections.Generic;
using SerializeReferenceEditor;

[System.Serializable]
//[CreateAssetMenu(menuName = "Data/Passive")]
public class PassiveSkillData // : ScriptableObject
{
    [field: SerializeReference, SR] public TriggerCondition TriggerCondition { get; private set; }
    [field: SerializeField] public AutoTargetEffect AutoTargetEffect { get; private set; }
    //[field: SerializeField] public bool UseAutoTarget { get; private set; } = true;
    [field: SerializeField] public bool UseActionSourceAsTarget { get; private set; } = false;
    [field: SerializeField] public bool UseActionTargetAsTarget { get; private set; } = false;
}