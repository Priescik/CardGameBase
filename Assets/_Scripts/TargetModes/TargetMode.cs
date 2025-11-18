using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;

[System.Serializable]
public class TargetMode
{
    [SerializeField] bool _use = false;
    [SerializeField] bool _filterSide = false;
    [SerializeField] SideSelect _side = SideSelect.All;
    [SerializeField] bool _filterType = false;
    [SerializeField] EntityTargetType _type;
    [SerializeField] bool _random = false;
    [SerializeField] int _randomCount = 1;
    [SerializeField] bool _otherConditions = false;

    //[field: SerializeReference, SR] TargetCondition[] _conditions;
    public List<EntityView> GetTargets(Side sourceSide)
    {
        Debug.Log("getTargets was called");
        List<EntityView> output = new();
        if (!_use) return output;

        var all = EntitySystem.Instance.All;
        foreach (var target in all)
        {
            //if (_filterSide && target.Side != sourceSide) continue;
            if (_filterSide && !SideSelector.Select(_side, sourceSide).Contains(target.Side)) continue;
            if (_filterType && !EntityTargetTypeMapper.IsAssignable(target.GetType(), _type)) continue;
            output.Add(target);
        }
        if (_random)
        {
            int n = output.Count;
            int q = Mathf.Min(_randomCount, n);

            for (int i = 0; i < q; i++)
            {
                int j = UnityEngine.Random.Range(i, n);
                (output[i], output[j]) = (output[j], output[i]);
            }

            return output.GetRange(0, q);
        }
        return output;
    }
}