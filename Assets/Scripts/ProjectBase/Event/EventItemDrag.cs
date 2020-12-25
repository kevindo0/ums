using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EventItemDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public void OnBeginDrag(PointerEventData eventData)
    {

    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = screenPos.z;
        Vector3 wordPos = Camera.main.ScreenToWorldPoint(mousePos);
        transform.position = wordPos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {

    }
}
