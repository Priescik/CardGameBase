using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;

public class ChangeStatsEffect : ManualTargetEffect
{
    [SerializeField] int _stat1Change;
    [SerializeField] int _stat2Change;
    [SerializeField] int _stat3Change;
    public override bool IsValidTarget(EntityView target) => target is MinionEntityView;
    public override Type GetValidType => typeof(MinionEntityView); // debug purposes

    public override GameAction GetGameAction(CardInstance cardSource, EntityView entitySource, List<EntityView> targets)
    {
        ChangeStatsGA changeStatsGA = new(_stat1Change, _stat2Change, _stat3Change, targets);
        return changeStatsGA;
    }
}
