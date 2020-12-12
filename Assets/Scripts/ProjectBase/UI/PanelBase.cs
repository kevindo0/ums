using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public abstract class PanelBase : MonoBehaviour
{
    private Dictionary<string, List<UIBehaviour>> uiDic = new Dictionary<string, List<UIBehaviour>>();

    private void Awake()
    {
        FindChildInComponent<Image>();
        FindChildInComponent<Button>();
        FindChildInComponent<Text>();
    }

    // 显示自己
    public virtual void ShowMe()
    {
        gameObject.SetActive(true);
    }

    // 隐藏自己
    public virtual void HideMe()
    {
        gameObject.SetActive(false);
    }

    /// <summary>
    /// 按名称获取指定类型的对象
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="name"></param>
    /// <returns></returns>
    protected T GetControl<T>(string name) where T: UIBehaviour
    {
        if(uiDic.ContainsKey(name))
        {
            for (int i = 0; i < uiDic[name].Count; i++)
            {
                if (uiDic[name][i] is T)
                    return uiDic[name][i] as T;
            }
        }
        return null;
    } 

    /// <summary>
    /// 将所有符合条件的子元素及本元素放入uiDic中
    /// </summary>
    /// <typeparam name="T"></typeparam>
    private void FindChildInComponent<T>() where T: UIBehaviour
    {
        T[] objs = this.GetComponentsInChildren<T>();
        foreach(var obj in objs)
        {
            if(uiDic.ContainsKey(obj.name))
            {
                uiDic[obj.name].Add(obj);
            } else
            {
                uiDic.Add(obj.name, new List<UIBehaviour>() { obj });
            }
        }
    }
}
