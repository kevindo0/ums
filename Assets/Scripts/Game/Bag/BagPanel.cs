using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BagPanel : MonoBehaviour
{
    public static int topTitle = 0; // 记录点击上部三个按钮的哪一个
    private static int oldTopTitle;

    private List<BagData> datas;
    private List<DecorateData> decorateDataes;

    public RectTransform content;
    public int viewPortH;

    private int count;

    void Start()
    {
        TitleCheck();
    }

    public void TitleSelect(int selected)
    {
        if (selected != oldTopTitle)
        {
            topTitle = selected;
            colorChange();
            TitleCheck();
        }
    }

    public void TitleCheck()
    {
        oldTopTitle = topTitle;
        destroyContent();
        switch (topTitle)
        {
            case 1:
                datas = BagMgr.Instance.InitItemsInfo();
                count = datas.Count;
                break;
            case 2:
                decorateDataes = BagMgr.Instance.InitDecorateInfo();
                count = decorateDataes.Count;
                break;
            default:
                decorateDataes = BagMgr.Instance.InitDecorateInfo();
                count = decorateDataes.Count;
                break;
        }
        CheckShowOrHide();
    }

    public void Active(bool isAcitve)
    {
        gameObject.SetActive(isAcitve);
    }

    public void CheckShowOrHide()
    {
        int minIndex = (int)(content.anchoredPosition.y / 190) * 3;
        int maxIndex = (int)((content.anchoredPosition.y + viewPortH) / 190) * 3 + 2;
        //Debug.Log(minIndex + ":" + maxIndex);
        for (int i = minIndex; i <= maxIndex; i++)
        {
            if (i >= count)
            {
                break;
            }
            switch (topTitle)
            {
                case 1:
                    GetGameObj(i);
                    break;
                default:
                    GetDecorateOjb(i);
                    break;
            }
        }
    }

    private GameObject GetGameObj(int i)
    {
        GameObject obj = Instantiate(Resources.Load<GameObject>("Prefabs/UI/ItemBG"));
        obj.GetComponent<BagItem>().InitItemInfo(datas[i]);
        obj.transform.SetParent(content);
        obj.transform.localScale = Vector3.one;
        obj.transform.localPosition = new Vector3((i % 3) * 180 + 95, -i / 3 * 185 - 95, 0);
        return obj;
    }

    private void GetDecorateOjb(int i)
    {
        GameObject obj = Instantiate(Resources.Load<GameObject>("Prefabs/UI/Decorate/Decorate"));
        obj.GetComponent<DecorateItem>().InitItemInfo(decorateDataes[i]);
        obj.transform.SetParent(content);
        obj.transform.localScale = Vector3.one;
        obj.transform.localPosition = new Vector3(i + 280, -i * 220 - 120, 0);
    }

    private void destroyContent()
    {
        for(int i = 0; i < content.childCount; ++i)
        {
            Destroy(content.GetChild(i).gameObject);
        }
    }

    private void colorChange()
    {
        Transform bg = gameObject.transform.GetChild(0);
        Transform top = bg.transform.GetChild(0);
        for (int i = 0; i < top.childCount; i++)
        {
            Color color;
            if(topTitle == i)
            {
                color = new Color32(31, 32, 43, 255);
            } else
            {
                color = new Color32(218, 86, 136, 255);
            }
            top.GetChild(i).transform.GetComponent<Image>().color = color;
        }
    }
}
