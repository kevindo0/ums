using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DecorateItem : MonoBehaviour
{
    private Transform content;

    public void InitItemInfo(DecorateData data)
    {
        Transform[] father = transform.GetComponentsInChildren<Transform>();
        foreach (var child in father)
        {
            switch (child.name)
            {
                case "Name":
                    child.GetComponent<Text>().text = data.title;
                    break;
                case "Content":
                    content = child.transform;
                    AddCells(content, data.useId, data.items);
                    break;
            }
        }
    }

    private void AddCells(Transform content, int userId, List<DecorateDataItem> itemData)
    {
        for(int i = 0; i < itemData.Count; ++i)
        {
            DecorateDataItem item = itemData[i];
            GameObject obj = Instantiate(Resources.Load<GameObject>("Prefabs/UI/Decorate/Item"));
            obj.name = "Item";
            obj.GetComponent<DecorateItemCell>().InitItemInfo(itemData[i], userId);
            obj.transform.SetParent(content);
            obj.transform.localScale = Vector3.one;
            obj.transform.localPosition = new Vector3(i * 120 + 60, -80, 0);
        }
    }
}
