using UnityEngine;
using System.Collections.Generic;

public class PerformCardAutoTargetEffectGA : GameAction, IHasTargets //, IHasEntitySource
{
    public CardInstance CardSource { get; private set; }
    public Effect Effect { get; private set; }
    public TargetMode TargetMode { get; private set; }
    public Side Side { get; private set; }
    public List<EntityView> Targets { get; private set; }
    public PerformCardAutoTargetEffectGA(CardInstance cardSource, Effect effect, TargetMode targetMode, Side side)
    {
        CardSource = cardSource; // TODO null check?
        Effect = effect;
        TargetMode = targetMode;
        Side = side;
        //Targets = TargetMode.GetTargets(Side);
    }
    public void SetTargets()
    {
        Targets = TargetMode.GetTargets(Side);
    }
}