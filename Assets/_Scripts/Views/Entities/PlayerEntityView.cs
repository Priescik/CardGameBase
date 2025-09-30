using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class PlayerEntityView : EntityView, IDamagable
{
    
    int _currentHealth;
    
    

    

    public override void Setup()
    {
        
        _spriteRenderer.sprite = _cardInstance.Image;
        RefreshView();
    }

    private void RefreshView()
    {
        _statText_3.text = _currentHealth.ToString() + "/" + _cardInstance.Stat3.ToString();
        //_spriteRenderer.sprite = _cardInstance.Image; // may be obsolete
    }

    public void OnMouseEnter()
    {
        if (!Interactions.Instance.PlayerCanHover()) return;
        //CardViewHoverSystem.Instance.ShowEntity(_cardInstance);
    }
    public void OnMouseExit()
    {
        //if (!Interactions.Instance.PlayerCanHover()) return;
        //CardViewHoverSystem.Instance.HideEntity();
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
        CardViewHoverSystem.Instance.HideEntity();
        foreach (PassiveSkillModel skill in _passives)
        {
           // TODO test if they unsub correctly
            skill.OnRemove();
        }
    }
}
