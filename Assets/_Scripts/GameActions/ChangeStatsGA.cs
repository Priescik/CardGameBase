using UnityEngine;
using System.Collections.Generic;

public class ChangeStatsGA : GameAction, IHasTargets
{
    public int Stat1Change;
    public int Stat2Change;
    public int Stat3Change;
    public List<EntityView> Targets { get; private set; }

    public ChangeStatsGA(int stat1Change, int stat2Change, int stat3Change, List<EntityView> targets)
    {
        Stat1Change = stat1Change;
        Stat2Change = stat2Change;
        Stat3Change = stat3Change;
        Targets = targets;
    }
}
