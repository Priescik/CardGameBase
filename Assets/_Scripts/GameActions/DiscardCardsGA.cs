using UnityEngine;

public class DiscardCardsGA : GameAction
{

    public int Amount { get; set; }
    public Player Player { get; set; }
    public DiscardCardsGA(int amount, Player player)
    {
        Amount = amount;
        Player = player;
    }
}
