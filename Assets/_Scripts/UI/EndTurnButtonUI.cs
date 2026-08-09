using UnityEngine;

public class EndTurnButtonUI : MonoBehaviour
{
    public void OnClick()
    {
        TurnSystem.Instance.EndTurn();
    }
}
