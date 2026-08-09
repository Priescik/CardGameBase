using UnityEngine;
using System;
public class Mana
{
    int _maximum = GameplayConfig.StartingMana;
    int _current = 0;

    public event Action<int> Changed;

    public Mana(int startingMana)
    {
        _maximum = startingMana;
        _current = 0;
    }

    public void Spend(int amount)
    {
        _current -= amount;
        Changed?.Invoke(_current);
    }
    public void Gain(int amount, bool refill) 
    {

        if (refill)
        {
            _current = _maximum;
        }
        else
        {
            _current += amount;
        }
        Changed?.Invoke(_current);
    }

    public void IncreaseCap(int amount)
    {
        _maximum += amount;
        if (_maximum > GameplayConfig.ManaHardCap)
            _maximum = GameplayConfig.ManaHardCap;
        Changed?.Invoke(_current);
    }

    public bool HasEnough(int amount) => _current >= amount;
}
