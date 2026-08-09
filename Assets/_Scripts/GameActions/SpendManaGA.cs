using UnityEngine;

public class SpendManaGA : GameAction
{
    public int Amount { get; set; }
    public Player Player { get; set; }
    public SpendManaGA(int amount, Player player)
    {
        Amount = amount;
        Player = player;
    }
}
