using UnityEngine;

public class CardViewCreator : Singleton<CardViewCreator>
{
    [SerializeField] CardView _cardViewPrefab;

    public CardView CreateCardView(CardInstance cardInstance, Vector3 position, Quaternion rotation)
    {
        CardView cardView = Instantiate(_cardViewPrefab, position, rotation);
        cardView.Setup(cardInstance);
        return cardView;
    }
}