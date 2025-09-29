using UnityEngine;
using System.Collections.Generic;

public class PerformEffectGA : GameAction, IHasEntitySource, IHasTargets
{
    public CardInstance CardSource;
    public EntityView EntitySource { get; private set; }
    public Effect Effect;
    public List<EntityView> Targets { get; private set; }
    public PerformEffectGA(CardInstance cardSource, EntityView entitySource, Effect effect, List<EntityView> targets)
    {
        CardSource = cardSource; // TODO null check?
        EntitySource = entitySource; // TODO null check?
        Effect = effect;
        Targets = targets == null ? null : new(targets);
    }
    public PerformEffectGA(CardInstance cardSource, Effect effect, List<EntityView> targets)
    {
        CardSource = cardSource; // TODO null check?
        EntitySource = null;
        Effect = effect;
        Targets = targets == null ? null : new(targets);
    }
    public PerformEffectGA(EntityView entitySource, Effect effect, List<EntityView> targets)
    {
        CardSource = null;
        EntitySource = entitySource; // TODO null check?
        Effect = effect;
        Targets = targets == null ? null : new(targets);
    }
}