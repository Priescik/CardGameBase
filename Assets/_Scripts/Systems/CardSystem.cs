using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class CardSystem : Singleton<CardSystem>
{
    void OnEnable()
    {
        ActionSystem.AttachPerformer<DrawCardsGA>(DrawCardsPerformer);
        ActionSystem.AttachPerformer<DiscardCardsGA>(DiscardAllCardsPerformer);
        ActionSystem.AttachPerformer<PlayCardGA>(PlayCardPerformer);
    }
    void OnDisable()
    {
        ActionSystem.DetachPerformer<DrawCardsGA>();
        ActionSystem.DetachPerformer<DiscardCardsGA>();
        ActionSystem.DetachPerformer<PlayCardGA>();
    }

    IEnumerator DrawCardsPerformer(DrawCardsGA drawCardsGA)
    {
        for (int i=0; i < drawCardsGA.Amount; i++)
        {
            foreach (var player in drawCardsGA.TargetPlayers)
            {
                if (player.DrawPile.Count < 1) break; //TODO handle end of deck
                yield return DrawCard(player);
            }
        }
    }
    IEnumerator DiscardAllCardsPerformer(DiscardCardsGA discardCardsGA)
    {
        foreach (var card in discardCardsGA.Player.Hand)
        {
            CardView cardView = discardCardsGA.Player.HandView.RemoveCard(card);
            discardCardsGA.Player.DiscardPile.Add(cardView.CardInstance);
            yield return MoveCardToDiscard(cardView);
        }
        discardCardsGA.Player.Hand.Clear();
    }

    IEnumerator PlayCardPerformer(PlayCardGA playCardGA)
    {
        Player ownerPlayer = playCardGA.CardInstance.Owner; // convenience
        ownerPlayer.Hand.Remove(playCardGA.CardInstance);
        CardView cardView = ownerPlayer.HandView.RemoveCard(playCardGA.CardInstance);
        ownerPlayer.DiscardPile.Add(cardView.CardInstance);
        yield return MoveCardToDiscard(cardView);

        SpendManaGA spendManaGA = new(playCardGA.CardInstance.Cost, ownerPlayer);
        ActionSystem.Instance.AddReaction(spendManaGA);

        if (playCardGA.CardInstance.ManualTargetEffect != null)
        {
            PerformCardEffectGA performEffectGA = new(playCardGA.CardInstance, playCardGA.CardInstance.ManualTargetEffect, new() { playCardGA.ManualTarget });
            ActionSystem.Instance.AddReaction(performEffectGA);
        }
        foreach (var effectWrapper in playCardGA.CardInstance.AutoTargetEffects)
        {
            PerformCardAutoTargetEffectGA performEffectGA = new(playCardGA.CardInstance, effectWrapper.Effect, effectWrapper.TargetMode, playCardGA.CardInstance.Owner.Side);
            ActionSystem.Instance.AddReaction(performEffectGA);
            // Use Reaction here, because another performer is already running (PlayCardGA)
        }
    }

    IEnumerator DrawCard(Player player)
    {
        CardInstance cardInstance = player.DrawPile.Draw();
        player.Hand.Add(cardInstance);
        CardView cardView = CardViewCreator.Instance.CreateCardView(player, cardInstance, player.DrawPilePoint.position, player.DrawPilePoint.rotation);
        yield return player.HandView.AddCard(cardView);
    }

    IEnumerator MoveCardToDiscard(CardView cardView)
    {
        cardView.transform.DOScale(Vector3.zero, 0.15f);
        //Tween tween = cardView.transform.DOMove(_discardPilePoint.position, 0.15f);
        Tween tween = cardView.transform.DOMove(Vector3.zero, 0.15f);
        yield return tween.WaitForCompletion();
        Destroy(cardView);
    }

}

