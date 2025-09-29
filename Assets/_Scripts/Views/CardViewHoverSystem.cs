using UnityEngine;

public class CardViewHoverSystem : Singleton<CardViewHoverSystem>
{
    [SerializeField] CardView _cardViewHover;
    [SerializeField] CardView _entityCardViewHover;

    #region Card
    public void Show(CardInstance cardInstance, Vector3 position, Quaternion rotation)
    {
        _cardViewHover.gameObject.SetActive(true);
        _cardViewHover.Setup(cardInstance);
        _cardViewHover.transform.position = position;
        _cardViewHover.transform.rotation = rotation;
    }

    public void Hide()
    {
        _cardViewHover.gameObject.SetActive(false);
    }
    #endregion

    #region Entity
    public void ShowEntity(CardInstance cardInstance)
    {
        _entityCardViewHover.gameObject.SetActive(true);
        _entityCardViewHover.Setup(cardInstance);
    }
    public void HideEntity()
    {
        _entityCardViewHover.gameObject.SetActive(false);
    }
    #endregion
}
