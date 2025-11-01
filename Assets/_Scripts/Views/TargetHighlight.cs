using UnityEngine;

public class TargetHighlight : MonoBehaviour
{
    Color _startColor;
    [SerializeField] Renderer _renderer;

    void OnStart()
    {
        if (HighlightingSystem.Instance != null) // sus
            HighlightingSystem.Instance.Register(this);
    }

    void OnEnable()
    {
        ////if (HighlightingSystem.Instance != null)
        //    HighlightingSystem.Instance.Register(this);
    }

    void OnDisable()
    {
        if (HighlightingSystem.Instance != null)
            HighlightingSystem.Instance.Unregister(this);
    }


    public void TurnOn(Color? color = null)
    {
        _startColor = _renderer.material.color; // TODO fix: multiple color saves my overlap resulting in visual bug - highlight will stay on
        Color targetColor = color ?? Color.white;
        targetColor.a = 1f;
        _renderer.material.color = targetColor;
    }

    public void TurnOff()
    {
        _renderer.material.color = _startColor;
    }

    //void OnMouseEnter()
    //{
    //    _startcolor = _renderer.material.color;
    //    _renderer.material.color = Color.yellow;
    //}

    //void OnMouseExit()
    //{
    //    // watch out for cases when OnMouseExit might not be called
    //    _renderer.material.color = _startcolor;
    //}
}
