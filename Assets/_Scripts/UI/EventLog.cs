using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventLog : MonoBehaviour
{
    [SerializeField] GameObject _entryPrefab;
    [SerializeField] Transform _rootContainer;
    ScrollRect _scrollRect;
    Transform _currentContainer;
    //EventEntryViewController _currentEntry;

    readonly Stack<EventEntryViewController> _entryStack = new();
    void Start()
    {
        _currentContainer = _rootContainer;
        _scrollRect = GetComponent<ScrollRect>();
        ActionSystem.Instance.ActionStarted += AddEntry;
        //ActionSystem.Instance.EnterPre += _ => StepInPre();
        ActionSystem.Instance.EnterPost += _ => StepInPost();
        ActionSystem.Instance.ActionFinished += _ => StepUp();
    }

    void AddEntry(GameAction gameAction)
    {
        GameObject newEventEntry = Instantiate(_entryPrefab, _currentContainer);
        EventEntryViewController entry = newEventEntry.GetComponent<EventEntryViewController>();
        entry.Setup(gameAction);
        _entryStack.Push(entry);

        StepInPre();

        // scroll down again
        if (_scrollRect.verticalNormalizedPosition < 0.1f)
        {
            Canvas.ForceUpdateCanvases();
            _scrollRect.verticalNormalizedPosition = 0f;
        }
    }

    void StepInPre()
    {
        _currentContainer = _entryStack.Peek()._preEventsContainer;
    }
    void StepInPost()
    {
        _currentContainer = _entryStack.Peek()._postEventsContainer;
    }
    void StepUp()
    {
        _entryStack.Pop();
        if (_entryStack.Count == 0)
        {
            _currentContainer = _rootContainer;
        }
        else
        {
            _currentContainer = _entryStack.Peek().parentContainer;
        }
    }
}
