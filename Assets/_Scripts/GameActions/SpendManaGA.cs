using UnityEngine;

public class SpendManaGA : GameAction
{
    public ResourceAmount Amount { get; set; }
    public Player Player { get; set; }
    public SpendManaGA(ResourceAmount amount, Player player)
    {
        Amount = amount;
        Player = player;
    }
}
