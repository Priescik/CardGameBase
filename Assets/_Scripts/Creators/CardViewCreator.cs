using UnityEngine;

public class CardViewCreator : Singleton<CardViewCreator>
{
    public IResourceAmountFormatter CostFormatter { get; private set; }
    [SerializeField] CardView _cardViewPrefab;

    override protected void Awake()
    {
        base.Awake();
        CostFormatter = new ResourceAmountFormatter();
    }

    public CardView CreateCardView(Player player, CardInstance cardInstance, Vector3 position, Quaternion rotation)
    {
        CardView cardView = Instantiate(_cardViewPrefab, position, rotation);
        cardView.SetFormatter(CostFormatter);
        cardView.Refresh(cardInstance);
        return cardView;
    }
}