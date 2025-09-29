using UnityEngine;
using System.Collections.Generic;

public class DestroyEntityGA : GameAction, IHasTargets
{
    public List<EntityView> Targets { get; private set; }
    public DestroyEntityGA(List<EntityView> targets)
    {
        Targets = new(targets);
    }
    public DestroyEntityGA(EntityView target)
    {
        Targets = new() { target };
    }
}
