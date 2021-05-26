using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GamePanelController : MonoBehaviour
{
    [SerializeField] private EventTrigger eventTrigger = default;

    public static Action<float> PointerUpAction = default;
    public static Action<float> OnDragAction = default;

    private Vector2 startMousePosition = default;

    private void Awake()
    {
        PrepareButtons();
    }

    private void PrepareButtons()
    {
        eventTrigger.triggers.Clear();
        var _pointerDown = new EventTrigger.Entry { eventID = EventTriggerType.PointerDown };
        _pointerDown.callback.AddListener((data) => {
            startMousePosition = Input.mousePosition;
        });

        var _pointerDrag = new EventTrigger.Entry { eventID = EventTriggerType.Drag };
        _pointerDrag.callback.AddListener((data) => {
            float currencySwipeDistance = Vector2.Distance(new Vector2(0f, startMousePosition.y), new Vector2(0f, Input.mousePosition.y));
            if (Input.mousePosition.y <= startMousePosition.y)
            {
                currencySwipeDistance = 0f;
            }
            OnDragAction?.Invoke(currencySwipeDistance);
        });

        var _pointerUp = new EventTrigger.Entry { eventID = EventTriggerType.PointerUp };
        _pointerUp.callback.AddListener((data) => {
            float currencySwipeDistance = Vector2.Distance(new Vector2(0f, startMousePosition.y), new Vector2(0f, Input.mousePosition.y));
            if (Input.mousePosition.y <= startMousePosition.y)
            {
                currencySwipeDistance = 0f;
            }
            PointerUpAction?.Invoke(currencySwipeDistance);
        });

        eventTrigger.triggers.Add(_pointerDown);
        eventTrigger.triggers.Add(_pointerDrag);
        eventTrigger.triggers.Add(_pointerUp);
    }
}
