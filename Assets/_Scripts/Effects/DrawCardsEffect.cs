using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using static UnityEngine.GraphicsBuffer;

public class DrawCardsEffect : Effect
{
    [SerializeField] int _drawAmount;

    public override GameAction GetGameAction(CardInstance cardSource, EntityView entitySource, List<EntityView> targets)
    {
        List <Player> targetPlayers = new List<Player>();
        foreach (var target in targets) {
            if (target is PlayerEntityView playerTarget)
            {
                targetPlayers.Add(playerTarget.Player);
            }
            else
            {
                Debug.LogWarning($"Target {target} is not a PlayerEntityView and will be ignored for DrawCardsGA. Might want to check why this even happened");
            } 
        }

        DrawCardsGA drawCardsGA = new(_drawAmount, targetPlayers);
        return drawCardsGA;
    }

}
