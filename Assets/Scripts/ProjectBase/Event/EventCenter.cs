using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventCenter : SingletonBase<EventCenter>
{
    private Dictionary<string, IEventInfo> eventDic = new Dictionary<string, IEventInfo>();

    // 添加监听函数 一个参数
    public void AddEventListener<T>(string name, UnityAction<T> action)
    {
        if (eventDic.ContainsKey(name))
            (eventDic[name] as EventInfo<T>).actions += action;
        else
            eventDic.Add(name, new EventInfo<T>(action));
    }

    // 添加监听函数 无参
    public void AddEventListener(string name, UnityAction action)
    {
        if(eventDic.ContainsKey(name))
            (eventDic[name] as EventInfo).actions += action;
        else
            eventDic.Add(name, new EventInfo(action));
    }

    // 监听事件触发 一个参数
    public void Trigger<T>(string name, T info)
    {
        if (eventDic.ContainsKey(name))
            (eventDic[name] as EventInfo<T>).actions.Invoke(info);
    }

    // 监听事件触发 无参
    public void Trigger(string name)
    {
        if (eventDic.ContainsKey(name))
            (eventDic[name] as EventInfo).actions.Invoke();
    }

    // 删除相应监听 一个参数
    public void RemoveEventListener<T>(string name, UnityAction<T> action)
    {
        if (eventDic.ContainsKey(name))
            (eventDic[name] as EventInfo<T>).actions -= action;
    }

    // 删除相应监听 无参
    public void RemoveEventListener(string name, UnityAction action)
    {
        if(eventDic.ContainsKey(name))
            (eventDic[name] as EventInfo).actions -= action;
    }

    public void Clear()
    {
        eventDic.Clear();
    }
}
