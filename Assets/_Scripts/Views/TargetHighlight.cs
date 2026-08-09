using UnityEngine;

public class TargetHighlight : MonoBehaviour
{
    [SerializeField] Renderer _renderer;
    public Color MouseoverColor;
    public Color PassiveColor;

    void OnStart()
    {
        if (HighlightingSystem.Instance != null) // sus
            HighlightingSystem.Instance.Register(this);
    }

    //void OnEnable()
    //{
        ////if (HighlightingSystem.Instance != null)
        //    HighlightingSystem.Instance.Register(this);
    //}

    void OnDisable()
    {
        if (HighlightingSystem.Instance != null)
            HighlightingSystem.Instance.Unregister(this);
    }


    public void TurnOn(Color color)
    {
        _renderer.enabled = true;
        _renderer.material.color = color ;
    }
    public void TurnOn()
    {
        _renderer.enabled = true;
        _renderer.material.color = VisualsConfig.DefaultHighlight;
    }
   
    public void TurnOff()
    {
        _renderer.enabled = false;
    }

    void OnMouseEnter()
    {
        if (MouseoverColor != null)
        {
            TurnOn(MouseoverColor);
        }
    }

    void OnMouseExit()
    {
        if (MouseoverColor != null)
        {
            TurnOn(PassiveColor);
        }
    }
}
