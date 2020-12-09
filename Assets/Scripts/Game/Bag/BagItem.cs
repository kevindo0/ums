using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEditor;

public class BagItem : MonoBehaviour
{
    public void InitItemInfo(BagData data)
    {
        Transform item = transform.GetChild(0);
        Transform[] father = item.GetComponentsInChildren<Transform>();
        foreach (var child in father)
        {
            switch (child.name)
            {
                case "Title":
                    child.GetComponent<Text>().text = data.title;
                    break;
                case "GameImg":
                    GetTexture(child, data.img);
                    break;
                case "Bottom":
                    GameObject obj;
                    if (data.isBuy)
                    {
                        obj = Instantiate(Resources.Load<GameObject>("Prefabs/UI/ItemStatus"));
                    }
                    else
                    {
                        obj = Instantiate(Resources.Load<GameObject>("Prefabs/UI/ItemMoney"));
                        obj.transform.GetChild(1).GetComponent<Text>().text = data.money.ToString();
                    }
                    obj.transform.SetParent(child);
                    obj.transform.localPosition = new Vector3(0, 0, 0);
                    obj.transform.localScale = new Vector3(1, 1, 1);
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
}
