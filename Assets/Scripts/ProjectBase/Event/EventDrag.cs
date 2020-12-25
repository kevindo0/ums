using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EventDrag : MonoBehaviour, IPointerDownHandler, IPointerClickHandler,  IPointerUpHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    void Start()
    {
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("click down");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("click");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("click up");
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Begin");
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Draging..." + Vector2.up);
        Debug.Log(eventData.delta);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("End");
    }
}
