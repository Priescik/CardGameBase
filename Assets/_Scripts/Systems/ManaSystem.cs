using UnityEngine;
using System.Collections;

public class ManaSystem : Singleton<ManaSystem>
{
    [SerializeField] ManaUI _manaUI;
    [SerializeField] ManaView _manaView;

    int _manaCapacity = 0; //TODO move to config
    int _currentMana = 0;

    void OnEnable()
    {
        ActionSystem.AttachPerformer<SpendManaGA>(SpendManaPerformer);
        ActionSystem.AttachPerformer<GainManaGA>(GainManaPerformer);
        ActionSystem.AttachPerformer<IncreaseManaCapGA>(IncreaseManaCapPerformer);
        ActionSystem.SubscribeReaction<EnemyTurnGA>(PostEnemyTurnManaRefillReaction, ReactionTiming.POST);
    }
    void OnDisable()
    {
        ActionSystem.DetachPerformer<SpendManaGA>();
        ActionSystem.DetachPerformer<GainManaGA>();
        ActionSystem.DetachPerformer<IncreaseManaCapGA>();
        ActionSystem.UnsubscribeReaction<EnemyTurnGA>(PostEnemyTurnManaRefillReaction, ReactionTiming.POST);
    }

    IEnumerator IncreaseManaCapPerformer(IncreaseManaCapGA increaseManaCapGA)
    {
        _manaCapacity += increaseManaCapGA.Amount;
        _manaUI.UpdateManaText(_currentMana); // added with "X/Y" mana format in mind 
        yield return null;
    }

    IEnumerator SpendManaPerformer(SpendManaGA spendManaGA)
    {
        _manaView.Subtract(spendManaGA.Amount);
        _currentMana -= spendManaGA.Amount;
        _manaUI.UpdateManaText(_currentMana);
        yield return null; //nothing to wait for
    }

    IEnumerator GainManaPerformer(GainManaGA gainManaGA)
    {
        if (gainManaGA.Refill) 
        {
            _manaView.Add(_manaCapacity - _currentMana);
            _currentMana = _manaCapacity;
        }
        else
        {
            _manaView.Add(gainManaGA.Amount);
            _currentMana += gainManaGA.Amount;
        }
        _manaUI.UpdateManaText(_currentMana);
        yield return null; //nothing to wait for

    }

    void PostEnemyTurnManaRefillReaction(EnemyTurnGA enemyTurnGA)
    {
        IncreaseManaCapGA increaseManaCapGA = new(1); //TODO config
        ActionSystem.Instance.AddReaction(increaseManaCapGA);
        GainManaGA gainManaGA = new(0, true);
        ActionSystem.Instance.AddReaction(gainManaGA);
    }

    public bool HasEnoughMana(int mana)
    {
        return _currentMana >= mana;
    }
}
