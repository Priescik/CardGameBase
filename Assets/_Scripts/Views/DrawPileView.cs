using UnityEngine;
using TMPro;

public class DrawPileView : MonoBehaviour
{
    [SerializeField] GameObject _drawPileTooltip;
    [SerializeField] TMP_Text _drawPileCountText;

    public void OnMouseEnter()
    {
        //if (!Interactions.Instance.PlayerCanHover()) return;
        _drawPileTooltip.SetActive(true);
        _drawPileCountText.text = CardSystem.Instance.DrawPileCount.ToString();
    }
    public void OnMouseExit()
    {
        //if (!Interactions.Instance.PlayerCanHover()) return;
        _drawPileTooltip.SetActive(false);
    }
}
