using UnityEngine;
using System.Collections.Generic;

public interface IHasTargets
{
    List<EntityView> Targets { get; }
}
