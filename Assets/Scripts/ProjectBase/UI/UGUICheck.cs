using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UGUICheck : SingletonBase<UGUICheck>
{
    public bool CheckGuiRaycastObject()
    {
        if (EventSystem.current != null)
        {
            PointerEventData eventData = new PointerEventData(EventSystem.current);
#if UNITY_EDITOR
            eventData.pressPosition = Input.mousePosition;
            eventData.position = Input.mousePosition;
#endif
            if (Application.platform == RuntimePlatform.Android)
            {
                if (Input.touchCount > 0)
                {
                    eventData.pressPosition = Input.GetTouch(0).position;
                    eventData.position = Input.GetTouch(0).position;
                }
            }
            List<RaycastResult> list = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, list);
            return list.Count > 0;
        }
        return false;
    }
}
