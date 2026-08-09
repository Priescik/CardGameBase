using UnityEngine;
using TMPro;

public class EventEntryViewController : MonoBehaviour
{
    [SerializeField] TMP_Text _text;
    [SerializeField] public Transform _preEventsContainer;
    [SerializeField] public Transform _postEventsContainer;
    public Transform parentContainer;
    GameAction _gameAction;

    void Start()
    {
        
    }

    public void Setup(GameAction gameAction)
    {
        _gameAction = gameAction;
        _text.text = gameAction.GetType().Name;
        parentContainer = transform.parent;
    }
}
