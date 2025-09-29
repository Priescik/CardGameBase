using UnityEngine;
using System.Collections;

public class EffectSystem : MonoBehaviour
{
    void OnEnable()
    {
        ActionSystem.AttachPerformer<PerformEffectGA>(PerformEffectPerformer);
    }
    void OnDisable()
    {
        ActionSystem.DetachPerformer<PerformEffectGA>();
    }
    IEnumerator PerformEffectPerformer(PerformEffectGA performEffectGA)
    {
        GameAction effectAction = performEffectGA.Effect.GetGameAction(performEffectGA.CardSource, performEffectGA.EntitySource, performEffectGA.Targets);
        ActionSystem.Instance.AddReaction(effectAction);
        yield return null;
    }
}
