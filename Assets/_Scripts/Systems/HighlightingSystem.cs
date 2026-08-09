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

    public void TurnOnValidCombatTargets(Side side)
    {
        foreach (EntityView entity in EntitySystem.Instance.All)
        {
            if (entity.TryGetComponent<TargetHighlight>(out TargetHighlight highlight))
            {
                if (entity.Side != side && EntityTargetTypeMapper.IsAssignable(entity.GetType(), EntityTargetType.MinionOrPlayer))
                {
                    highlight.PassiveColor = VisualsConfig.ValidTargetHighlight;
                    highlight.MouseoverColor = VisualsConfig.MouseTargetHighlight;
                    highlight.TurnOn();
                }
            }
        }
    }

    public void TurnOnValidTargets(ManualTargetEffect effect)
    {
        foreach (EntityView entity in EntitySystem.Instance.All)
        {
            if (entity.TryGetComponent<TargetHighlight>(out TargetHighlight highlight))
            {
                if (effect.IsValidTarget(entity))
                {
                    highlight.PassiveColor = VisualsConfig.ValidTargetHighlight;
                    highlight.MouseoverColor = VisualsConfig.MouseTargetHighlight;
                    highlight.TurnOn();
                }
            }
        }
    }

    //public void TurnOnMouseTarget(EntityView entity, bool isValid)
    //{
    //    if (entity.TryGetComponent<TargetHighlight>(out TargetHighlight targetHighlight))
    //    {
    //        Color color = isValid ? VisualsConfig.MouseTargetHighlight : VisualsConfig.InvalidMouseTargetHighlight;
    //        targetHighlight.TurnOn(color);
    //    }
    //}

    public void TurnOffAll()
    {
        foreach (TargetHighlight h in _highlights)
        {
            h.TurnOff();
            h.PassiveColor = VisualsConfig.DefaultHighlight;
            h.MouseoverColor = VisualsConfig.DefaultHighlight;
        }
    }
}
