using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class MatchSetupSystem : Singleton<MatchSetupSystem>
{
    // TODO refactor
    [SerializeField] List<CardTemplate> _deckData;
    // PLAYER 1
    [Header("Player 1 views")]
    [SerializeField] PlayerEntityView _playerEntityView;
    [SerializeField] HandView _handView;
    [SerializeField] DiscardPileView _discardPileView;
    [SerializeField] DrawPileView _drawPileView;
    [SerializeField] ManaView _manaView;
    // PLAYER 2
    //[SerializeField] List<CardTemplate> _deckData2;
    [Header("Player 2 views")]
    [SerializeField] PlayerEntityView _playerEntityView2;
    [SerializeField] HandView _handView2;
    [SerializeField] DiscardPileView _discardPileView2;
    [SerializeField] DrawPileView _drawPileView2;
    [SerializeField] ManaView _manaView2;
    Player player1;
    Player player2;

    void Start()
    {
        player1 = new Player(Side.A, _deckData, _playerEntityView, _handView, _discardPileView, _drawPileView); // TODO Bind views
        player2 = new Player(Side.B, _deckData, _playerEntityView2, _handView2, _discardPileView2, _drawPileView2);
        _manaView.Bind(player1.Mana);
        _manaView2.Bind(player2.Mana);
        EntitySystem.Instance.InitializeEmptyEntities(player1, player2);
        _playerEntityView.Setup(null, player1); // TODO refactor
        _playerEntityView2.Setup(null, player2);
        EntitySystem.Instance.AddPlayersEntities(new List<PlayerEntityView>() { _playerEntityView, _playerEntityView2 });

        TurnSystem.Instance.SetPlayers(player1, player2);

        //ActionSystem.Instance.Perform(drawCardsGA);

        //IncreaseManaCapGA increaseManaCapGA = new(1);
        //ActionSystem.Instance.Perform(increaseManaCapGA);
        //GainManaGA gainManaGA = new(1, true);
        //ActionSystem.Instance.Perform(gainManaGA);

        //SetupSequence2(player1);
        //SetupSequence2(player2);
        ActionSystem.Instance.PerformSequence(new GameAction[]
        {
            new DrawCardsGA(GameplayConfig.StartingHandSize, player1),
            new DrawCardsGA(GameplayConfig.StartingHandSize, player2),
            new IncreaseManaCapGA(GameplayConfig.StartingMana, player1),
            new IncreaseManaCapGA(GameplayConfig.StartingMana, player2),
            new GainManaGA(0, player1, true),
            new GainManaGA(0, player2, true),
        });
    }
    void SetupSequence2(Player player)
    {
        DrawCardsGA drawCardsGA = new(GameplayConfig.StartingHandSize, player);
        ActionSystem.Instance.Perform(drawCardsGA, () =>
        {
            IncreaseManaCapGA increaseManaCapGA = new(GameplayConfig.StartingMana, player);
            ActionSystem.Instance.Perform(increaseManaCapGA, () =>
            {
                GainManaGA gainManaGA = new(0, player, true);
                ActionSystem.Instance.Perform(gainManaGA);
            });
        });
    }


    IEnumerator SetupSequence(Player player) /// Depreciated
    {
        bool done = false;

        DrawCardsGA drawCardsGA = new(GameplayConfig.StartingHandSize, player);
        ActionSystem.Instance.Perform(drawCardsGA, () => done = true);
        yield return new WaitUntil(() => done);

        done = false;
        // This was added to run UI update, however it might
        // become a config-based setup in the future
        IncreaseManaCapGA increaseManaCapGA = new(GameplayConfig.StartingMana, player);
        ActionSystem.Instance.Perform(increaseManaCapGA, () => done = true);
        yield return new WaitUntil(() => done);

        done = false;
        GainManaGA gainManaGA = new(0, player, true);
        ActionSystem.Instance.Perform(gainManaGA, () => done = true);
        yield return new WaitUntil(() => done);
    }

    public Player GetPlayerBySide(Side side)
    {
        if (side == player1.Side) return player1;
        if (side == player2.Side) return player2;
        return null;
    }
}
