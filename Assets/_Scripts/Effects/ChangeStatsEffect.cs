using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;

public class ChangeStatsEffect : ManualTargetEffect
{
    [SerializeField]
    [AllowedEntityTargetTypes(EntityTargetType.Minion)]
    EntityTargetType _allowedTargetType;
    protected override EntityTargetType AllowedTargetType => _allowedTargetType;

    [SerializeField] int _stat1Change;
    [SerializeField] int _stat2Change;
    [SerializeField] int _stat3Change;

    public override GameAction GetGameAction(CardInstance cardSource, EntityView entitySource, List<EntityView> targets)
    {
        ChangeStatsGA changeStatsGA = new(_stat1Change, _stat2Change, _stat3Change, targets);
        return changeStatsGA;
    }
}
