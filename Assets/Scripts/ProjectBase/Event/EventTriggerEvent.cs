using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;


/// <summary>
/// 给对象代码手动添加 EventTrigger 事件
/// 继承此类进行方法添加
/// </summary>
public class EventTriggerEvent : MonoBehaviour
{
    protected void AddTriggerEvent(Component obj, EventTriggerType eventType, UnityAction<BaseEventData> callback)
    {
        EventTrigger.Entry entry = null;
        bool triggerAdd = true;  // 检测是否要重新添加 entry

        EventTrigger trigger = obj.GetComponent<EventTrigger>();

        if(trigger != null)
        {
            // 查看是否已经存在要注册的事件
            foreach(EventTrigger.Entry existingEntry in trigger.triggers)
            {
                if(existingEntry.eventID == eventType)
                {
                    entry = existingEntry;
                    triggerAdd = false;
                    break;
                }
            }
        } else
        {
            trigger = obj.gameObject.AddComponent<EventTrigger>();
            // 初始化EventTrigger.Entry容器
            trigger.triggers = new List<EventTrigger.Entry>();
        }

        if(entry == null)
        {
            // 初始化EventTrigger.Entry对象
            entry = new EventTrigger.Entry();
            entry.eventID = eventType;
            entry.callback = new EventTrigger.TriggerEvent();
        }
        entry.callback.AddListener(callback);
        if(triggerAdd)
            trigger.triggers.Add(entry);
    }
}
