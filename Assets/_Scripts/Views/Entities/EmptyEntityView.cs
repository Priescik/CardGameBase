using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class EmptyEntityView : EntityView
{
    [SerializeField] Sprite _emptySprite;

    public override void Setup(CardInstance cardInstance, Side side)
    {
        _spriteRenderer.sprite = _emptySprite;
        Side = side;
    }
}
