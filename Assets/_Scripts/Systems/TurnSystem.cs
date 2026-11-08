using UnityEngine;
using System.Collections;

public class TurnSystem : Singleton<TurnSystem>
{

    public Player CurrentPlayer { get; private set; }
    Player _nextPlayer;
    void OnEnable()
    {
        ActionSystem.AttachPerformer<StartTurnGA>(StartTurnPerformer);
        ActionSystem.AttachPerformer<EndTurnGA>(EndTurnPerformer);
    }

    void OnDisable()
    {
        ActionSystem.DetachPerformer<StartTurnGA>();
        ActionSystem.DetachPerformer<EndTurnGA>();
    }

    public void EndTurn() 
    {
        EndTurnGA endTurnGA = new(CurrentPlayer);
        StartTurnGA startTurnGA = new(_nextPlayer);
        ActionSystem.Instance.Perform(endTurnGA, () => ActionSystem.Instance.Perform(startTurnGA));
    }

    IEnumerator StartTurnPerformer(StartTurnGA startTurnGA)
    {
        ActionSystem.Instance.AddReaction(new IncreaseManaCapGA(GameplayConfig.ManaIncreasePerTurn, startTurnGA.Player));
        ActionSystem.Instance.AddReaction(new GainManaGA(GameplayConfig.StartingManaAmount, startTurnGA.Player, true)); // this is gameplay specific logic, TODO consider moving to new script eg. "TurnRules"
        ActionSystem.Instance.AddReaction(new DrawCardsGA(GameplayConfig.CardDrawPerTurn, startTurnGA.Player));
        yield return new WaitForSeconds(0.5f);
    }
    IEnumerator EndTurnPerformer(EndTurnGA endTurnGA)
    {
        (CurrentPlayer, _nextPlayer) = (_nextPlayer, CurrentPlayer);
        yield return new WaitForSeconds(0.5f);
    }

    public void SetPlayers(Player firstPlayer, Player secondPlayer)
    {
        CurrentPlayer = firstPlayer;
        _nextPlayer = secondPlayer;
    }
}
