using UnityEngine;

public class IncreaseManaCapGA : GameAction
{
    public int Amount { get; set; }
    public IncreaseManaCapGA(int amount)
    {
        Amount = amount;
    }
}