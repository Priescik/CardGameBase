using UnityEngine;

public class CardViewHoverSystem : Singleton<CardViewHoverSystem>
{
    [SerializeField] CardView _cardViewHover;
    [SerializeField] CardView _entityCardViewHover;

    #region Card
    public void Show(CardInstance cardInstance, Transform basePos)
    {
        Quaternion rot = Quaternion.Euler(basePos.localEulerAngles.x, 0f, 0f);
        Vector3 pos = basePos.position + rot * (Vector3.up * 6 + Vector3.back * 0.2f);
        _cardViewHover.gameObject.SetActive(true);
        _cardViewHover.Setup(cardInstance);
        _cardViewHover.transform.position = pos;
        _cardViewHover.transform.rotation = rot;
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
