using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class MatchSetupSystem : MonoBehaviour
{
    [SerializeField] int _startingHandSize;
    [SerializeField] int _startingManaCapacity;
    [SerializeField] List<CardTemplate> _deckData;

    void Start()
    {
        EntitySystem.Instance.InitializeEmptyEntities();

        CardSystem.Instance.SetupDrawPile(_deckData);
        //DrawCardsGA drawCardsGA = new(5); // TODO move value to config
        //ActionSystem.Instance.Perform(drawCardsGA);

        //IncreaseManaCapGA increaseManaCapGA = new(1);
        //ActionSystem.Instance.Perform(increaseManaCapGA);
        //GainManaGA gainManaGA = new(1, true);
        //ActionSystem.Instance.Perform(gainManaGA);
        StartCoroutine(SetupSequence());
    }

    IEnumerator SetupSequence()
    {
        bool done = false;

        CardSystem.Instance.SetupDrawPile(_deckData);
        DrawCardsGA drawCardsGA = new(_startingHandSize); // TODO move value to config
        ActionSystem.Instance.Perform(drawCardsGA, () => done = true);
        yield return new WaitUntil(() => done);

        done = false;
        // This was added to run UI update, however it might
        // become a config-based setup in the future
        IncreaseManaCapGA increaseManaCapGA = new(_startingManaCapacity);
        ActionSystem.Instance.Perform(increaseManaCapGA, () => done = true);
        yield return new WaitUntil(() => done);

        done = false;
        GainManaGA gainManaGA = new(0, true);
        ActionSystem.Instance.Perform(gainManaGA, () => done = true);
        yield return new WaitUntil(() => done);

    }
}
