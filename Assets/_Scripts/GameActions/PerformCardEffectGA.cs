using UnityEngine;
using System.Collections.Generic;

public class PerformCardEffectGA : GameAction, IHasTargets //, IHasEntitySource
{
    public CardInstance CardSource;
    //public EntityView EntitySource { get; private set; }
    public Effect Effect;
    public List<EntityView> Targets { get; private set; }
    //
    //public bool SetTargetsOnResolve { get; private set; }
    //public TargetMode TargetMode { get; private set; }
    //
    //public PerformCardEffectGA(CardInstance cardSource, EntityView entitySource, Effect effect, List<EntityView> targets)
    //{
    //    CardSource = cardSource; // TODO null check?
    //    //EntitySource = entitySource; // TODO null check?
    //    Effect = effect;
    //    Targets = targets == null ? null : new(targets);
    //}
    public PerformCardEffectGA(CardInstance cardSource, Effect effect, List<EntityView> targets)
    {
        CardSource = cardSource; // TODO null check?
        //EntitySource = null;
        Effect = effect;
        Targets = targets == null ? null : new(targets);
    }
    //public PerformCardEffectGA(EntityView entitySource, Effect effect, List<EntityView> targets)
    //{
    //    CardSource = null;
    //    //EntitySource = entitySource; // TODO null check?
    //    Effect = effect;
    //    Targets = targets == null ? null : new(targets);
    //}
}