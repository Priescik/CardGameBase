using UnityEngine;
using System;

public class ResourceAmountFormatter : IResourceAmountFormatter
{
    public string Format(ResourceAmount amount)
    {
        return amount switch
        {
            IntegerManaAmount integer => integer.Amount.ToString(),
            DiceManaAmount dice => FormatDice(dice),
            _ => throw new ArgumentException(
                $"Unsupported resource amount: {amount.GetType().Name}")
        };
    }

    private string FormatDice(DiceManaAmount amount)
    {
        string output = "";
        foreach (var a in amount.amounts)
        {
            output += a.ToString(); // TODO implement creation of UI gameobject
        }
        return output;
    }
}