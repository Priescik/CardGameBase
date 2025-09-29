using UnityEngine;
using System.Collections.Generic;
using SerializeReferenceEditor;

[CreateAssetMenu(fileName = "New CardTemplate", menuName = "CardTemplate")]
public class CardTemplate : ScriptableObject
{
    [field: SerializeField] public string CardName { get; private set; }
    [field: SerializeField] public string Description { get; private set; }
    [field: SerializeField] public CardType CardType { get; private set; }
    [field: SerializeField] public int Cost { get; private set; }
    [field: SerializeField] public int Stat1 { get; private set; }
    [field: SerializeField] public int Stat2 { get; private set; }
    [field: SerializeField] public int Stat3 { get; private set; }
    [field: SerializeField] public Sprite Image { get; private set; }
    [field: SerializeReference, SR] public ManualTargetEffect ManualTargetEffect { get; private set; } = null;
    [field: SerializeField] public List<AutoTargetEffect> AutoTargetEffects { get; private set; }
    [field: SerializeField] public List<PassiveSkillData> PassivesData { get; private set; }
}
