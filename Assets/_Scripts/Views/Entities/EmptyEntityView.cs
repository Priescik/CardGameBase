using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class EmptyEntityView : EntityView
{
    [SerializeField] Sprite _emptySprite;

    public override void Setup(CardInstance cardInstance)
    {
        _spriteRenderer.sprite = _emptySprite;
    }
}
