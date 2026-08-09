using UnityEngine;

public class IncreaseManaCapGA : GameAction
{
    public int Amount { get; private set; }
    public Player Player { get; private set; }
    public IncreaseManaCapGA(int amount, Player player)
    {
        Amount = amount;
        Player = player;
    }
}