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
        Vector3 pos = transform.position + transform.localRotation * Vector3.up * 4;
        CardViewHoverSystem.Instance.Show(CardInstance, pos, transform.rotation);
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

        if (CardInstance.ManualTargetEffect != null)
        {
            TargetingSystem.Instance.StartTargeting(transform.position);
        }
        else
        {
            Interactions.Instance.PlayerIsDragging = true;
            _wrapper.SetActive(true);
            CardViewHoverSystem.Instance.Hide();
            //SetVisibilityLayer("Default");
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
        if (CardInstance.ManualTargetEffect != null) return;
        // TODO this should probably trigger non-play OnMouseUp effect, just in case the CanInteract turns off while dragging
        transform.position = MouseRaycastSystem.Instance.GetMouseOnPlane();
    }

    void OnMouseUp()
    {
        if (!Interactions.Instance.PlayerCanInteract()) return;

        //LayerMask rayLayer = CardInstance.RequiresTargetEntity() ? _basicDropLayerMask : _targetedDropLayerMask;

        if (CardInstance.ManualTargetEffect != null)
        {
            if (!ManaSystem.Instance.HasEnoughMana(CardInstance.Cost))
            {
                Debug.Log("Not enough Mana!"); // TODO game view cue
                return;
            }
            EntityView target = TargetingSystem.Instance.EndTargeting(MouseRaycastSystem.Instance.GetMouseOnPlane());
            if (target != null && CardInstance.ManualTargetEffect.IsValidTarget(target))
            {
                PlayCardGA playCardGA = new(CardInstance, target);
                ActionSystem.Instance.Perform(playCardGA);
            }
            else
            {
                Debug.Log($"Wrong target. Correct target is: {CardInstance.ManualTargetEffect.GetValidType}");
                return;
            }
        }
        else
        {
            //TODO move this to drag so that visual cue of playability can be displayed (eg. glow)
            if (ManaSystem.Instance.HasEnoughMana(CardInstance.Cost)
                && Physics.Raycast(transform.position, -Vector3.up, out RaycastHit hit, 10f, _basicDropLayerMask))
            {
                //if (CardInstance.RequiresTargetEntity() && false) {
                //    if (hit.rigidbody.gameObject.TryGetComponent<EntityView>(out EntityView targetEntityView)) {// play card
                //    }else {// return card
                //    }
                //}
                //else
                {
                    PlayCardGA playCardGA = new(CardInstance);
                    ActionSystem.Instance.Perform(playCardGA);
                }
            }
            else
            {
                if (ManaSystem.Instance.HasEnoughMana(CardInstance.Cost)){
                    Debug.Log("Not enough Mana!");
                }
                else
                {
                    // Vulnerability: this functionality depends heavily on relation of DragPlane and DropArea positions
                    Debug.Log("In case you missed card drop plane - check if DragPlane and DropArea are positioned correctly in relation to each other");
                }
                transform.position = _dragStartPosition;
                transform.rotation = _dragStartRotation;
            }
            Interactions.Instance.PlayerIsDragging = false;
        }

    }
}
