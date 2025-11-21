using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class CardSystem : Singleton<CardSystem>
{
    [SerializeField] HandView _handView;
    [SerializeField] Transform _discardPilePoint;
    [SerializeField] Transform _drawPilePoint;
    //---
    List<CardInstance> _drawPile = new();
    public int DrawPileCount => _drawPile.Count;
    List<CardInstance> _discardPile = new();
    public int DiscardPileCount => _discardPile.Count;
    List<CardInstance> _hand = new();

    // TODO differentiate players!!!
    Side _side_TODO = Side.A;

    void OnEnable()
    {
        ActionSystem.AttachPerformer<DrawCardsGA>(DrawCardsPerformer);
        ActionSystem.AttachPerformer<DiscardCardsGA>(DiscardAllCardsPerformer);
        ActionSystem.AttachPerformer<PlayCardGA>(PlayCardPerformer);
        ActionSystem.SubscribeReaction<EnemyTurnGA>(EnemyTurnPostReaction, ReactionTiming.POST);
        
    }
    void OnDisable()
    {
        ActionSystem.DetachPerformer<DrawCardsGA>();
        ActionSystem.DetachPerformer<DiscardCardsGA>();
        ActionSystem.DetachPerformer<PlayCardGA>();
        ActionSystem.UnsubscribeReaction<EnemyTurnGA>(EnemyTurnPostReaction, ReactionTiming.POST);
    }

    public void SetupDrawPile(List<CardTemplate> deckData)
    {
        foreach (var cardTemplate in deckData)
        {
            CardInstance cardInstance = new(cardTemplate);
            _drawPile.Add(cardInstance);
        }
    }

    IEnumerator DrawCardsPerformer(DrawCardsGA drawCardsGA)
    {
        for (int i=0; i < drawCardsGA.Amount; i++)
        {
            if (_drawPile.Count < 1) break; //TODO handle end of deck
            yield return DrawCard();
        }
    }
    IEnumerator DiscardAllCardsPerformer(DiscardCardsGA discardCardsGA)
    {
        foreach (var card in _hand)
        {
            CardView cardView = _handView.RemoveCard(card);
            yield return MoveCardToDiscard(cardView);
        }
        _hand.Clear();
    }

    IEnumerator PlayCardPerformer(PlayCardGA playCardGA)
    {
        _hand.Remove(playCardGA.CardInstance);
        CardView cardView = _handView.RemoveCard(playCardGA.CardInstance);
        yield return MoveCardToDiscard(cardView);

        SpendManaGA spendManaGA = new(playCardGA.CardInstance.Cost);
        ActionSystem.Instance.AddReaction(spendManaGA);

        if (playCardGA.CardInstance.ManualTargetEffect != null)
        {
            PerformCardEffectGA performEffectGA = new(playCardGA.CardInstance, playCardGA.CardInstance.ManualTargetEffect, new() { playCardGA.ManualTarget });
            ActionSystem.Instance.AddReaction(performEffectGA);
        }
        foreach (var effectWrapper in playCardGA.CardInstance.AutoTargetEffects)
        {
            PerformCardAutoTargetEffectGA performEffectGA = new(playCardGA.CardInstance, effectWrapper.Effect, effectWrapper.TargetMode, _side_TODO);
            ActionSystem.Instance.AddReaction(performEffectGA);
            // Use Reaction here, because another performer is already running (PlayCardGA)
        }
    }

    void EnemyTurnPostReaction(EnemyTurnGA enemyTurnGA)
    {
        DrawCardsGA drawCardsGA = new(1);
        ActionSystem.Instance.AddReaction(drawCardsGA); // TODO should be defined as turn structure
    }

    IEnumerator DrawCard()
    {
        CardInstance cardInstance = _drawPile.Draw();
        _hand.Add(cardInstance);
        CardView cardView = CardViewCreator.Instance.CreateCardView(cardInstance, _drawPilePoint.position, _drawPilePoint.rotation);
        yield return _handView.AddCard(cardView);
    }

    IEnumerator MoveCardToDiscard(CardView cardView)
    {
        _discardPile.Add(cardView.CardInstance);
        cardView.transform.DOScale(Vector3.zero, 0.15f);
        Tween tween = cardView.transform.DOMove(_discardPilePoint.position, 0.15f);
        yield return tween.WaitForCompletion();
        Destroy(cardView);
    }

}

