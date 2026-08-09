using UnityEngine;
using System.Collections;

public class EnemySystem : MonoBehaviour
{
    /// <summary>
    /// Implement this for asymetrical PvE gameplay
    /// </summary>
    void OnEnable()
    {
        ActionSystem.AttachPerformer<EnemyTurnGA>(EnemyTurnPerformer);
        ActionSystem.SubscribeReaction<EnemyTurnGA>(PostEnemyTurnManaRefillReaction, ReactionTiming.POST);
    }

    void OnDisable()
    {

        ActionSystem.DetachPerformer<EnemyTurnGA>();
        ActionSystem.UnsubscribeReaction<EnemyTurnGA>(PostEnemyTurnManaRefillReaction, ReactionTiming.POST);
    }

    IEnumerator EnemyTurnPerformer(EnemyTurnGA enemyTurnGA)
    {
        Debug.Log("Enemy Turn Start");
        yield return new WaitForSeconds(2f);
    }

    void PostEnemyTurnManaRefillReaction(EnemyTurnGA enemyTurnGA)
    {
        // Main player reference is needed
        //IncreaseManaCapGA increaseManaCapGA = new(GameplayConfig.ManaGainPerTurn);
        //ActionSystem.Instance.AddReaction(increaseManaCapGA);
        //GainManaGA gainManaGA = new(0, true);
        //ActionSystem.Instance.AddReaction(gainManaGA);
    }
}
