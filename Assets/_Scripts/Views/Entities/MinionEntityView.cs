using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class MinionEntityView : EntityView, IDamagable
{
    [SerializeField] TMP_Text _statText_1;
    [SerializeField] TMP_Text _statText_2;
    [SerializeField] TMP_Text _statText_3;
    int _currentHealth;
    CardInstance _cardInstance;
    //Dict<string, int> _intStats;
    //public int GetIntStat(string name) {  return _intStats[name]; }

    List<PassiveSkillModel> _passives;

    public override void Setup(CardInstance cardInstance, Side side)
    {
        Side = side;
        _cardInstance = cardInstance;
        _passives = new();
        foreach (PassiveSkillData skillData in _cardInstance.PassivesData)
        {
            PassiveSkillModel skillModel = new(skillData, this);
            _passives.Add(skillModel); // TODO test if they unsub correctly
            skillModel.OnAdd();
        }

        _currentHealth = _cardInstance.Stat3;
        _spriteRenderer.sprite = _cardInstance.Image;
        RefreshView();
    }

    private void RefreshView()
    {
        _statText_1.text = _cardInstance.Stat1.ToString();
        _statText_2.text = _cardInstance.Stat2.ToString();
        _statText_3.text = _currentHealth.ToString() + "/" + _cardInstance.Stat3.ToString();
        //_spriteRenderer.sprite = _cardInstance.Image; // may be obsolete
    }

    public void OnMouseEnter()
    {
        if (!Interactions.Instance.PlayerCanHover()) return;
        CardViewHoverSystem.Instance.ShowEntity(_cardInstance);
    }
    public void OnMouseExit()
    {
        //if (!Interactions.Instance.PlayerCanHover()) return;
        CardViewHoverSystem.Instance.HideEntity();
    }

    public void ApplyStatChanges(int stat1Change, int stat2Change, int stat3Change)
    {
        _cardInstance.Stat1 += stat1Change;
        _cardInstance.Stat2 += stat2Change;
        _currentHealth += stat3Change; // this behaviour depends on game design
        _cardInstance.Stat3 += stat3Change;
        RefreshView();
    }

    public bool TakeDamage(int amount)
    {
        ///
        /// Returns bool if damage was fatal
        /// 
        _currentHealth -= amount;
        RefreshView();
        return _currentHealth <= 0;
    }



    void OnDestroy()
    {
        foreach (PassiveSkillModel skill in _passives)
        {
           // TODO test if they unsub correctly
            skill.OnRemove();
        }
        CardViewHoverSystem.Instance.HideEntity(); // TODO only if this entity is being shown
    }
}
