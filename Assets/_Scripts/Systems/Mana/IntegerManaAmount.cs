using UnityEngine;

public class IntegerManaAmount : ResourceAmount
{
    [SerializeField] public int Amount;

    public IntegerManaAmount()
    {
        this.Amount = 0;
    }
    public IntegerManaAmount(int amount)
    {
        this.Amount = amount;
    }   

    public static IntegerManaAmount operator +(IntegerManaAmount a, IntegerManaAmount b)
    {
        return new IntegerManaAmount(a.Amount + b.Amount);
    }

    public static bool operator >(IntegerManaAmount a, IntegerManaAmount b)
    {
        return a.Amount > b.Amount;
    }

    public static bool operator <(IntegerManaAmount a, IntegerManaAmount b)
    {
        return a.Amount < b.Amount;
    }

    public static bool operator >(IntegerManaAmount a, int b)
    {
        return a.Amount > b;
    }

    public static bool operator <(IntegerManaAmount a, int b)
    {
        return a.Amount < b;
    }
    public static bool operator >=(IntegerManaAmount a, IntegerManaAmount b)
    {
        return a.Amount > b.Amount;
    }

    public static bool operator <=(IntegerManaAmount a, IntegerManaAmount b)
    {
        return a.Amount < b.Amount;
    }

    public static bool operator >=(IntegerManaAmount a, int b)
    {
        return a.Amount > b;
    }

    public static bool operator <=(IntegerManaAmount a, int b)
    {
        return a.Amount < b;
    }

    public static implicit operator IntegerManaAmount(int value)
    {
        return new IntegerManaAmount(value);
    }
    public static implicit operator int(IntegerManaAmount value)
    {
        return value.Amount;
    }
}
