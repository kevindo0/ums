using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class DecorateItemCell : MonoBehaviour
{
    private DecorateDataItem __data;
    private string __activeName;

    public void InitItemInfo(DecorateDataItem data, int userId)
    {
        __data = data;

        Transform[] father = transform.GetComponentsInChildren<Transform>();
        foreach (var child in father)
        {
            switch (child.name)
            {
                case "Title":
                    child.GetComponent<Text>().text = data.objname;
                    break;
                case "Body":
                    GetTexture(child.transform, data.image);
                    break;
                case "Bottom":
                    ChangeActive(child.transform, data, userId);
                    break;
            }
        }
    }

    void GetTexture(Transform ts, string path)
    {
        Image img = ts.GetComponent<Image>();
        Texture2D t = (Texture2D)AssetDatabase.LoadAssetAtPath(path, typeof(Texture2D));
        Sprite sprite = Sprite.Create(t, new Rect(0, 0, t.width, t.height), new Vector2(0.5f, 0.5f));
        img.sprite = sprite;
    }

    void ChangeActive(Transform obj, DecorateDataItem data, int userId)
    {
        string activeName = "Money";
        if(userId == data.id)
        {
            activeName = "Use";
        } else if (data.isBuy)
        {
            activeName = "Bought";
        }
        this.__activeName = activeName;

        for(int i = 0; i < obj.transform.childCount; ++i)
        {
            if(obj.GetChild(i).name == activeName)
            {
                obj.GetChild(i).gameObject.SetActive(true);
            } else
            {
                obj.GetChild(i).gameObject.SetActive(false);
            }
        }
    }

    // 点击图片购买物品
    public void UpdateDesk()
    {
        if(__activeName == "Money")
        {
            // 检查主角游戏币值是否充足
            Debug.Log(__data.image);
        }
    }
}
