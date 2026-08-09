using UnityEngine;
using TMPro;
using System.Security.Principal;

public class CardView : MonoBehaviour
{
    [SerializeField] GameObject _wrapper;
    [SerializeField] TMP_Text _cardName;
    [SerializeField] TMP_Text _description;
    [SerializeField] TMP_Text _cost;
    [SerializeField] TMP_Text _stat1;
    [SerializeField] TMP_Text _stat2;
    [SerializeField] TMP_Text _stat3;
    [SerializeField] SpriteRenderer _spriteRenderer;
    [SerializeField] LayerMask _basicDropLayerMask;
    [SerializeField] LayerMask _targetedDropLayerMask;
    // TODO cardtype box

    public CardInstance CardInstance { get; private set; }

    Vector3 _dragStartPosition;
    Quaternion _dragStartRotation;
    public void Setup(CardInstance cardInstance)
    {
        CardInstance = cardInstance;
        _cardName.text = cardInstance.Name;
        _description.text = cardInstance.Description;
        _cost.text = cardInstance.Cost.ToString();
        _stat1.text = cardInstance.Stat1.ToString();
        _stat2.text = cardInstance.Stat2.ToString();
        _stat3.text = cardInstance.Stat3.ToString();
        _spriteRenderer.sprite = cardInstance.Image;
    }

    //public void SetVisibilityLayer(string layerName)
    //{
    //    Helpers.SetLayerRecursively(_wrapper, layerName);
    //}

    public void OnMouseEnter()
    {
        if (!Interactions.Instance.PlayerCanHover()) return;
        _wrapper.SetActive(false);
        //Vector3 pos = transform.position + transform.localRotation * (Vector3.up * 6 + Vector3.back * 0.2f); 
        // kept rotation requires edge cards to be displaced more for the corder to be visible and it looks odd
        CardViewHoverSystem.Instance.Show(CardInstance, transform);
    }
    public void OnMouseExit()
    {
        if (!Interactions.Instance.PlayerCanHover()) return;
        CardViewHoverSystem.Instance.Hide();
        _wrapper.SetActive(true);
    }
    void OnMouseDown()
    {
        if (!Interactions.Instance.PlayerCanInteract()) return;

        if (CardInstance.UsesManualTarget)
        {
            TargetingSystem.Instance.StartTargeting(transform.position);
            HighlightingSystem.Instance.TurnOnValidTargets(CardInstance.ManualTargetEffect);
        }
        else
        {
            Interactions.Instance.PlayerIsDragging = true;
            _wrapper.SetActive(true);
            CardViewHoverSystem.Instance.Hide();
            _dragStartPosition = transform.position;
            _dragStartRotation = transform.rotation;
            Vector3 lookDirection = transform.position - Camera.main.transform.position;
            lookDirection.x = 0;
            transform.position = MouseRaycastSystem.Instance.GetMouseOnPlane();
            transform.rotation = Quaternion.LookRotation(lookDirection);
        }
    }

    void OnMouseDrag()
    {
        if (!Interactions.Instance.PlayerCanInteract()) return;
        if (CardInstance.UsesManualTarget)
        {
            // TODO store changed highlight and revert this one only
            //EntityView target = TargetingSystem.Instance.GetTarget(MouseRaycastSystem.Instance.GetMouseOnPlane());
            //if (target != null)
            //{
            //    HighlightingSystem.Instance.TurnOnMouseTarget(target, CardInstance.ManualTargetEffect.IsValidTarget(target));
            //}
        }
        else
        {
            transform.position = MouseRaycastSystem.Instance.GetMouseOnPlane() + Vector3.up * VisualsConfig.CardDragHeight;
        }
    }

    void OnMouseUp()
    {

        if (CardInstance.UsesManualTarget)
        {
            HighlightingSystem.Instance.TurnOffAll();
        }

        if (!Interactions.Instance.PlayerCanInteract()) return;

        if (CardInstance.UsesManualTarget)
        {
            EntityView target = TargetingSystem.Instance.EndTargeting(MouseRaycastSystem.Instance.GetMouseOnPlane());
            if (!CardInstance.Owner.Mana.HasEnough(CardInstance.Cost))
            {
                Debug.Log("Not enough Mana!"); // TODO visual cue
                return;
            }
            if (target != null && CardInstance.ManualTargetEffect.IsValidTarget(target))
            {
                PlayCardGA playCardGA = new(CardInstance, target);
                ActionSystem.Instance.Perform(playCardGA);
            }
            else
            {
                Debug.Log($"Wrong target. Correct target is/are: {CardInstance.ManualTargetEffect.GetValidType}. What was hit: {target}");
                return;
            }
        }
        else
        {
            if (CardInstance.Owner.Mana.HasEnough(CardInstance.Cost))
            {
                if (Physics.Raycast(transform.position, -Vector3.up, out RaycastHit hit, 100f, _basicDropLayerMask))
                {
                    PlayCardGA playCardGA = new(CardInstance);
                    ActionSystem.Instance.Perform(playCardGA);
                }
                else
                {
                    // Vulnerability: this functionality depends heavily on relation of DragPlane and DropArea positions
                    Debug.Log("In case you missed card drop plane - check if DragPlane and DropArea are positioned correctly in relation to each other");
                    transform.position = _dragStartPosition;
                    transform.rotation = _dragStartRotation;
                }
            }
            else
            {
                Debug.Log("Not enough Mana!");
                transform.position = _dragStartPosition;
                transform.rotation = _dragStartRotation;
            }
            Interactions.Instance.PlayerIsDragging = false;
        }

    }
}
