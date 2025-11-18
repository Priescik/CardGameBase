using UnityEngine;
using System.Collections;

public class EffectSystem : MonoBehaviour
{
    void OnEnable()
    {
        ActionSystem.AttachPerformer<PerformCardEffectGA>(PerformCardEffectPerformer);
        ActionSystem.AttachPerformer<PerformCardAutoTargetEffectGA>(PerformCardAutoTargetEffectPerformer);
    }
    void OnDisable()
    {
        ActionSystem.DetachPerformer<PerformCardEffectGA>();
        ActionSystem.DetachPerformer<PerformCardAutoTargetEffectGA>();
    }
    IEnumerator PerformCardEffectPerformer(PerformCardEffectGA performCardEffectGA)
    {
        GameAction effectAction = performCardEffectGA.Effect.GetGameAction(performCardEffectGA.CardSource, null, performCardEffectGA.Targets);
        ActionSystem.Instance.AddReaction(effectAction);
        yield return null;
    }
    IEnumerator PerformCardAutoTargetEffectPerformer(PerformCardAutoTargetEffectGA performCardAutoTargetEffectGA)
    {
        // if performEffectGA...LateTargeting targets = GetTargets // czy PerformEffect mo¿e przechowywaæ TargetMode?
        performCardAutoTargetEffectGA.SetTargets();
        GameAction effectAction = performCardAutoTargetEffectGA.Effect.GetGameAction(performCardAutoTargetEffectGA.CardSource, null, performCardAutoTargetEffectGA.Targets);
        ActionSystem.Instance.AddReaction(effectAction);
        yield return null;
    }
}
