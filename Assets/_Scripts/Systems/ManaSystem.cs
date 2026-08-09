using UnityEngine;
using System.Collections;

public class ManaSystem : Singleton<ManaSystem>
{
    void OnEnable()
    {
        ActionSystem.AttachPerformer<SpendManaGA>(SpendManaPerformer);
        ActionSystem.AttachPerformer<GainManaGA>(GainManaPerformer);
        ActionSystem.AttachPerformer<IncreaseManaCapGA>(IncreaseManaCapPerformer);
    }
    void OnDisable()
    {
        ActionSystem.DetachPerformer<SpendManaGA>();
        ActionSystem.DetachPerformer<GainManaGA>();
        ActionSystem.DetachPerformer<IncreaseManaCapGA>();
    }

    IEnumerator IncreaseManaCapPerformer(IncreaseManaCapGA increaseManaCapGA)
    {
        increaseManaCapGA.Player.Mana.IncreaseCap(increaseManaCapGA.Amount);
        yield return null;
    }

    IEnumerator SpendManaPerformer(SpendManaGA spendManaGA)
    {
        spendManaGA.Player.Mana.Spend(spendManaGA.Amount);
        yield return null; //nothing to wait for
    }

    IEnumerator GainManaPerformer(GainManaGA gainManaGA)
    {
        gainManaGA.Player.Mana.Gain(gainManaGA.Amount, gainManaGA.Refill);
        yield return null; //nothing to wait for
    }
}
