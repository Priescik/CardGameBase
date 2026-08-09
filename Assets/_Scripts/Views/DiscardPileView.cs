using UnityEngine;
using TMPro;

public class DiscardPileView : MonoBehaviour
{
    [SerializeField] GameObject _discardPileTooltip;
    [SerializeField] TMP_Text _discardPileCountText;

    public void OnMouseEnter()
    {
        //if (!Interactions.Instance.PlayerCanHover()) return;
        _discardPileTooltip.SetActive(true);
        _discardPileCountText.text = "this class has no info about the player yet";//CardSystem.Instance.DiscardPileCount.ToString();
    }
    public void OnMouseExit()
    {
        //if (!Interactions.Instance.PlayerCanHover()) return;
        _discardPileTooltip.SetActive(false);
    }
}