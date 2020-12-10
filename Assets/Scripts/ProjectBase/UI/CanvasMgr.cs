using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// UI 分层
/// </summary>
public enum UI_Layer
{
    BOT,
    MID,
    TOP
}

public class CanvasMgr : SingletonMono<CanvasMgr>
{
    private Transform top;
    private Transform mid;
    private Transform bot;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
        top = transform.Find("Top");
        mid = transform.Find("Mid");
        bot = transform.Find("Bot");
    }

    /// <summary>
    /// 获取画板
    /// </summary>
    /// <returns>画板</returns>
    public Transform GetCanvas()
    {
        return gameObject.transform;
    }

    /// <summary>
    /// 获取层对象
    /// </summary>
    /// <param name="layer"></param>
    /// <returns></returns>
    public Transform GetLayer(UI_Layer layer)
    {
        switch(layer)
        {
            case UI_Layer.BOT:
                return bot;
            case UI_Layer.MID:
                return mid;
            case UI_Layer.TOP:
                return top;
        }
        return null;
    }

    /// <summary>
    /// 将对象设置挂载在某层对象上
    /// </summary>
    /// <param name="obj">挂载对象</param>
    /// <param name="layer">层对象</param>
    public void SetParent(GameObject obj, UI_Layer layer)
    {
        Transform parent = GetLayer(layer);
        if (parent != null)
        {
            obj.transform.SetParent(parent);
        }
    }
}
