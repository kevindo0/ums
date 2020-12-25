using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// 嵌套 Scroll Rect 组件的拖拽顺序
/// 将此脚本放置到内层ScrollRect上
/// </summary>
public class NestedScrollRect : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public ScrollRect anotherScrollRect;

    public bool upOrDown;

    private ScrollRect selfScrollRect;

    private void Awake()
    {
        selfScrollRect = GetComponent<ScrollRect>();
        anotherScrollRect = GetComponentInParent<ScrollRect>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {

        anotherScrollRect.OnBeginDrag(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        anotherScrollRect.OnDrag(eventData);
        float angle = Vector2.Angle(eventData.delta, Vector2.up);
        if(angle > 45f && angle < 135f)
        {
            selfScrollRect.enabled = !upOrDown;
            anotherScrollRect.enabled = upOrDown;
        } else
        {
            selfScrollRect.enabled = upOrDown;
            anotherScrollRect.enabled = !upOrDown;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        anotherScrollRect.OnEndDrag(eventData);
        anotherScrollRect.enabled = true;
        selfScrollRect.enabled = true;
    }
}
