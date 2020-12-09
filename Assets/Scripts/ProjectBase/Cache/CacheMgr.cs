using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CacheCell
{
    private GameObject parent;
    private Stack<GameObject> cells;

    public CacheCell(GameObject p)
    {
        parent = p;
        cells = new Stack<GameObject>();
    }

    // 取出一个游戏对象
    public GameObject PopItem(string resName)
    {
        if (cells.Count > 0)
        {
            GameObject popobj = cells.Pop();
            popobj.transform.parent = null;
            // 状态激活
            popobj.SetActive(true);
            return popobj;
        }
        GameObject obj = GameObject.Instantiate(Resources.Load<GameObject>(resName));
        obj.name = resName;
        return obj;
    }

    // 压入一个游戏对象
    public void PushItem(GameObject obj)
    {
        obj.transform.parent = parent.transform;
        // 失活
        obj.SetActive(false);
        cells.Push(obj);
    }
}

// 缓存管理
public class CacheMgr : SingletonBase<CacheMgr>
{
    private GameObject cacheEmpty;

    private Dictionary<string, CacheCell> pool = new Dictionary<string, CacheCell>();

    // 出栈
    public GameObject Pop(string resName)
    {
        if(cacheEmpty == null)
        {
            cacheEmpty = new GameObject();
            cacheEmpty.name = "CacheEmpty";
        }
        if (pool.ContainsKey(resName))
        {
            pool[resName].PopItem(resName);
        } else
        {
            GameObject p = new GameObject();
            p.name = resName;
            p.transform.parent = cacheEmpty.transform;
            CacheCell cell = new CacheCell(p);
            cell.PopItem(resName);
            pool[resName] = cell;
        }
        return cacheEmpty;
    }

    // 压入栈
    public void Push(GameObject obj)
    {
        string name = obj.name;
        Debug.Log(name);
        if(pool.ContainsKey(name))
        {
            pool[name].PushItem(obj);
        }
    }
}
