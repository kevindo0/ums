using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TestEventTrigger : EventTriggerEvent
{
    void Start()
    {
        
        AddTriggerEvent(transform, EventTriggerType.PointerClick, PointerClick);
        AddTriggerEvent(transform, EventTriggerType.PointerClick, PointerClick2);
        AddTriggerEvent(transform, EventTriggerType.PointerClick, PointerClick3);
    }

    void Update()
    {
        
    }

    public void PointerClick(BaseEventData eventData)
    {
        Debug.Log("click 1");
    }

    public void PointerClick2(BaseEventData eventData)
    {
        Debug.Log("click 2");
    }

    public void PointerClick3(BaseEventData eventData)
    {
        Debug.Log("click 3");
    }
}
