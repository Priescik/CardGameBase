using System.Collections.Generic;
using UnityEngine;

public class DrawCardsGA : GameAction
{
    public int Amount { get; set; }
    //public Player TargetPlayer { get; set; }
    public List<Player> TargetPlayers { get; private set; }
    public DrawCardsGA(int amount, List<Player> targets)
    {
        Amount = amount;
        TargetPlayers = targets;
    }
    public DrawCardsGA(int amount, Player target)
    {
        Amount = amount;
        TargetPlayers = new List<Player>() { target };
    }
}
