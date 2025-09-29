using UnityEngine;
using System.Collections;

public class EnemySystem : MonoBehaviour
{
    void OnEnable()
    {
        ActionSystem.AttachPerformer<EnemyTurnGA>(EnemyTurnPerformer);
    }

    void OnDisable()
    {

        ActionSystem.DetachPerformer<EnemyTurnGA>();
    }

    IEnumerator EnemyTurnPerformer(EnemyTurnGA enemyTurnGA)
    {
        Debug.Log("Enemy Turn Start");
        yield return new WaitForSeconds(2f);
        Debug.Log("Enemy Turn End");
    }
}
