using UnityEngine;

public class PlayCardGA : GameAction
{
    public CardInstance CardInstance { get; set; }
    public EntityView ManualTarget { get; set; }
    public PlayCardGA(CardInstance cardInstance)
    {
        CardInstance = cardInstance;
        ManualTarget = null;
    }
    public PlayCardGA(CardInstance cardInstance, EntityView manualTarget)
    {
        CardInstance = cardInstance;
        ManualTarget = manualTarget;
    }
}
