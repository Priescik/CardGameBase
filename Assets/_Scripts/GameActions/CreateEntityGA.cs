using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CreateEntityGA : GameAction
{
    public CardInstance CardInstance;
    public EntityView TargetEntity;

    public CreateEntityGA( CardInstance cardInstance, EntityView target)
    {
        CardInstance = cardInstance;
        TargetEntity = target;
    }
    public CreateEntityGA(EntityView target)
    {
        CardInstance = null;
        TargetEntity = target;
    }

}
