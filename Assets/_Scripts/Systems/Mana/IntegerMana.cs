using UnityEngine;
using System;
public class IntegerMana : Resource
{
    IntegerManaAmount _maximum;
    IntegerManaAmount _current;

    public event Action<IntegerManaAmount> Changed;

    public IntegerMana(IntegerManaAmount startingMana)
    {
        _maximum = startingMana;
        _current = startingMana;
    }

    public override void Spend(ResourceAmount amount)
    {
        _current.Amount -= ((IntegerManaAmount)amount).Amount;
        Changed?.Invoke(_current);
    }
    public override void Gain(ResourceAmount amount, bool refill)
    {

        if (refill)
        {
            _current = _maximum;
        }
        else
        {
            _current += (IntegerManaAmount)amount;
        }
        Changed?.Invoke(_current);
    }

    public void IncreaseCap(IntegerManaAmount amount)
    {
        _maximum += amount;
        if (_maximum > GameplayConfig.ManaHardCap)
            _maximum = GameplayConfig.ManaHardCap;
        Changed?.Invoke(_current);
    }

    public override bool HasEnough(ResourceAmount amount) {
        return ((IntegerManaAmount)amount).Amount <= _current;
    }
}
