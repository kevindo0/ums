using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            UIMgr<TestPannel>.Instance.ShowPanel("TestPanel", UI_Layer.BOT);
        }
        if(Input.GetMouseButtonDown(1))
        {
            UIMgr<TestPannel>.Instance.HidePanel("TestPanel");
        }
    }

    public void Click()
    {
        //GameObject img = Instantiate(Resources.Load<GameObject>("image"));
        //CanvasMgr.Instance.SetParent(img, UI_Layer.BOT);
    }
}
