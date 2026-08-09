using UnityEngine;

public class GainManaGA : GameAction
{
    public int Amount { get; private set; }
    public bool Refill { get; private set; }
    public Player Player { get; private set; }
    public GainManaGA(int amount, Player player, bool refill=false)
    {
        Amount = amount;
        Refill = refill;
        Player = player;
    }
}
