using UnityEngine;
using System.Collections;

public class TurnSystem : Singleton<TurnSystem>
{
    Player _currentPlayer;
    Player _nextPlayer;
    void OnEnable()
    {
        ActionSystem.AttachPerformer<StartTurnGA>(StartTurnPerformer);
        ActionSystem.AttachPerformer<EndTurnGA>(EndTurnPerformer);
        //ActionSystem.SubscribeReaction<EndTurnGA>(PostEndTurnStartTurnReaction, ReactionTiming.POST);
    }

    void OnDisable()
    {
        ActionSystem.DetachPerformer<StartTurnGA>();
        ActionSystem.DetachPerformer<EndTurnGA>();
        //ActionSystem.SubscribeReaction<EndTurnGA>(PostEndTurnStartTurnReaction, ReactionTiming.POST);
    }

    public void EndTurn() 
    {
        EndTurnGA endTurnGA = new(_currentPlayer);
        StartTurnGA startTurnGA = new(_nextPlayer);
        ActionSystem.Instance.Perform(endTurnGA, () => ActionSystem.Instance.Perform(startTurnGA));
    }

    IEnumerator StartTurnPerformer(StartTurnGA startTurnGA)
    {
        ActionSystem.Instance.AddReaction(new IncreaseManaCapGA(GameplayConfig.ManaIncreasePerTurn, startTurnGA.Player));
        ActionSystem.Instance.AddReaction(new GainManaGA(0, startTurnGA.Player, true)); // this is gameplay specific logic, TODO consider moving to new script eg. "TurnRules"
        ActionSystem.Instance.AddReaction(new DrawCardsGA(GameplayConfig.CardDrawPerTurn, startTurnGA.Player));
        yield return new WaitForSeconds(0.5f);
    }
    IEnumerator EndTurnPerformer(EndTurnGA endTurnGA)
    {
        (_currentPlayer, _nextPlayer) = (_nextPlayer, _currentPlayer);
        yield return new WaitForSeconds(0.5f);
    }

    //void PostEndTurnStartTurnReaction(EndTurnGA endTurnGA)
    //{
    //    StartTurnGA startTurnGA = new();
    //    ActionSystem.Instance.Perform(startTurnGA);
    //}

    public void SetPlayers(Player firstPlayer, Player secondPlayer)
    {
        _currentPlayer = firstPlayer;
        _nextPlayer = secondPlayer;
    }
}
