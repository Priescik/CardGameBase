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
    public EntityView EndTargeting(Vector3 endPosition)
    {
        _arrowView.gameObject.SetActive(false);
        if (Physics.Raycast(endPosition, Vector3.down, out RaycastHit hit, 10f, _targetLayerMask)
            && hit.collider != null
            && hit.transform.TryGetComponent(out EntityView entityView))
        {
            return entityView;
        }
        return null;
    }
}
