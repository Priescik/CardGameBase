using UnityEngine;

public class GainManaGA : GameAction
{
    public int Amount { get; set; }
    public bool Refill { get; set; }
    public GainManaGA(int amount, bool refill=false)
    {
        Amount = amount;
        Refill = refill;
    }
}
