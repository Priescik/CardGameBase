using UnityEngine;
using System.Collections.Generic;
using SerializeReferenceEditor;

[System.Serializable]
//[CreateAssetMenu(menuName = "Data/Passive")]
public class PassiveSkillData
{
    [field: SerializeReference, SR] public TriggerCondition TriggerCondition { get; private set; }
    [field: SerializeField] public AutoTargetEffect AutoTargetEffect { get; private set; }
    //[field: SerializeField] public bool UseAutoTarget { get; private set; } = true;
    [field: SerializeField] public bool AddActionSourceAsTarget { get; private set; } = false;
    [field: SerializeField] public bool AddActionTargetAsTarget { get; private set; } = false;
    [field: SerializeField] public bool AddPassiveOwnerAsTarget { get; private set; } = false;
}