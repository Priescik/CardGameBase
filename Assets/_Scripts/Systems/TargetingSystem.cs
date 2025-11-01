using UnityEngine;

public class TargetingSystem : Singleton<TargetingSystem>
{
    [SerializeField] ArrowView _arrowView;
    [SerializeField] LayerMask _targetLayerMask;
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public void StartTargeting(Vector3 startPosition)
    {
        _arrowView.gameObject.SetActive(true);
        _arrowView.SetupArrow(startPosition);
    }

    /// <summary>
    /// Returns entityView hit by raycast under endPosition, without ending the animation.
    /// 
    /// This might be used while Targeting is ongoing and before it's end.
    /// </summary>
    /// <param name="endPosition">Coordinates there the mouse (or pointer) is</param>
    /// <returns></returns>
    public EntityView GetTarget(Vector3 endPosition)
    {
        if (Physics.Raycast(endPosition, Vector3.down, out RaycastHit hit, 10f, _targetLayerMask)
            && hit.collider != null
            && hit.transform.TryGetComponent(out EntityView entityView))
        {
            return entityView;
        }
        return null;
    }
    /// <summary>
    /// Ends animation and returns entityView hit by raycast under endPosition
    /// </summary>
    /// <param name="endPosition">Coordinates there the mouse (or pointer) is</param>
    /// <returns></returns>
    public EntityView EndTargeting(Vector3 endPosition)
    {
        _arrowView.gameObject.SetActive(false);
        return GetTarget(endPosition);
    }
}
