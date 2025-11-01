using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class PlayerEntityView : EntityView, IDamagable
{
    [SerializeField] TMP_Text _healthText;
    int _currentHealth;
    
    public override void Setup(CardInstance cardInstance, Side side)
    {
        Side = side;
        _spriteRenderer.sprite = cardInstance.Image;
        RefreshView();
    }

    private void RefreshView()
    {
        _healthText.text = _currentHealth.ToString();
        //_spriteRenderer.sprite = _cardInstance.Image; // may be obsolete
    }

    public void OnMouseEnter()
    {
        //if (!Interactions.Instance.PlayerCanHover()) return;
        //CardViewHoverSystem.Instance.ShowEntity(_cardInstance);
    }
    public void OnMouseExit()
    {
        //if (!Interactions.Instance.PlayerCanHover()) return;
        //CardViewHoverSystem.Instance.HideEntity();
    }


    ///
    /// Returns bool if damage was fatal
    /// 
    public bool TakeDamage(int amount)
    {
        _currentHealth -= amount;
        RefreshView();
        return _currentHealth <= 0;
    }
}
