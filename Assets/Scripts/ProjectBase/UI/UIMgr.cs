using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMgr<T> : SingletonBase<UIMgr<T>> where T: PanelBase
{
    // 面板字典
    private Dictionary<string, T> _panel = new Dictionary<string, T>();

    public void ShowPanel(string prefabpath, UI_Layer layer)
    {
        if(_panel.ContainsKey(prefabpath))
        {
            _panel[prefabpath].ShowMe();
            return;
        }
        GameObject obj = GameObject.Instantiate(Resources.Load<GameObject>(prefabpath));
        Transform parent = CanvasMgr.Instance.GetLayer(layer);
        // 设置父对象
        obj.transform.SetParent(parent);
        // 设置相对位置和大小
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localScale = Vector3.one;

        RectTransform rectObj = obj.transform as RectTransform;
        rectObj.offsetMax = Vector2.zero;
        rectObj.offsetMin = Vector2.zero;

        // 得到面板脚本
        T panel = obj.GetComponent<T>();
        _panel.Add(prefabpath, panel);
    }

    public void HidePanel(string prefabpath)
    {
        if(_panel.ContainsKey(prefabpath))
        {
            _panel[prefabpath].HideMe();
        }
    }
}
