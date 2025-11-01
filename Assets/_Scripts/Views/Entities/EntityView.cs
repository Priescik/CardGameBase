using UnityEngine;

public abstract class EntityView : MonoBehaviour
{
    [SerializeField] protected SpriteRenderer _spriteRenderer;
    public abstract void Setup(CardInstance card, Side side);
    public Side Side { get; protected set; }
}
