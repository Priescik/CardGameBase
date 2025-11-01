using UnityEngine;
using System.Collections.Generic;

public class HighlightingSystem : Singleton<HighlightingSystem>
{
    readonly List<TargetHighlight> _highlights = new List<TargetHighlight>();

    public void Register(TargetHighlight highlight)
    {
        if (!_highlights.Contains(highlight))
            _highlights.Add(highlight);
    }

    public void Unregister(TargetHighlight highlight)
    {
        _highlights.Remove(highlight);
    }

    public void TurnOnPossibleTargets(ManualTargetEffect effect)
    {
        foreach (TargetHighlight h in _highlights)
            h.TurnOn();
    }

    public void TurnOnMouseTarget(EntityView entity, bool isValid)
    {
        if (entity.TryGetComponent<TargetHighlight>(out TargetHighlight targetHighlight))
        {
            Color color = isValid ? Color.green : Color.red;
            targetHighlight.TurnOn(color);
        }
    }

    public void TurnOffAll()
    {
        foreach (TargetHighlight h in _highlights)
            h.TurnOff();
    }
}
