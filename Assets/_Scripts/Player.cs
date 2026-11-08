using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public PlayerEntityView PlayerEntityView { get; private set; } // TODO refactor; private set?
    public HandView HandView { get; private set; }
    public Transform DiscardPilePoint { get; private set; } // TODO move to visuals file
    public Transform DrawPilePoint { get; private set; } // TODO move to visuals file
    //---
    public List<CardInstance> DrawPile = new();
    public List<CardInstance> DiscardPile = new();
    public List<CardInstance> Hand = new();
    public int DiscardPileCount => DiscardPile.Count;
    public int DrawPileCount => DrawPile.Count;
    public int HandCount => Hand.Count;
    public IntegerMana Mana { get; private set; }

    public Side Side { get; private set; }
    public string Name => Side == Side.A ? "Player 1" : "Player 2"; // debug purposes for now
    public override string ToString() => Name;

    public Player(Side side, 
        List<CardTemplate> deckData, 
        PlayerEntityView playerEntityView, 
        HandView handView,
        DiscardPileView discardPileView,
        DrawPileView drawPileView,
        IntegerMana mana)
    {
        Side = side;
        SetDrawPile(deckData);
        PlayerEntityView = playerEntityView;
        HandView = handView;
        DiscardPilePoint = discardPileView.transform;
        DrawPilePoint = drawPileView.transform;

        Mana = mana;
    }

    private void SetDrawPile(List<CardTemplate> deckData)
    {
        Debug.Log("Creating deck for player: " + this);
        foreach (CardTemplate cardTemplate in deckData)
        {
            CardInstance cardInstance = new(cardTemplate, this);
            DrawPile.Add(cardInstance);
        }
    }
}
