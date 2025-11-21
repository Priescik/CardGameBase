using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class DrawCardsEffect : Effect
{
    [SerializeField] int _drawAmount;

    public override GameAction GetGameAction(CardInstance cardSource, EntityView entitySource, List<EntityView> targets)
    {
        // TODO target player
        DrawCardsGA drawCardsGA = new(_drawAmount);
        return drawCardsGA;
    }

}
