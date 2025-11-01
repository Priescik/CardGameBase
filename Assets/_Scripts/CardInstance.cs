using UnityEngine;
using System.Collections.Generic;

public class CardInstance
{
    readonly CardTemplate _cardTemplate;
    // wont be changed
    public string Name => _cardTemplate.CardName; 
    public string Description => _cardTemplate.Description; 
    public CardType CardType => _cardTemplate.CardType;
    public Sprite Image => _cardTemplate.Image;
    public ManualTargetEffect ManualTargetEffect => _cardTemplate.ManualTargetEffect;
    public List<AutoTargetEffect> AutoTargetEffects => _cardTemplate.AutoTargetEffects;
    public List<PassiveSkillData> PassivesData => _cardTemplate.PassivesData;
    // can be changed
    public int Cost {get; set;} 
    public int Stat1 {get; set;} 
    public int Stat2 { get; set;} 
    public int Stat3 { get; set; }

    public CardInstance(CardTemplate cardTemplate)
    {
        _cardTemplate = cardTemplate;
        Cost = cardTemplate.Cost;
        Stat1 = cardTemplate.Stat1;
        Stat2 = cardTemplate.Stat2;
        Stat3 = cardTemplate.Stat3;
    }

    public bool UsesManualTarget => this.ManualTargetEffect != null;
}
