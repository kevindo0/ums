using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITest : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            EventCenter.Instance.Trigger("main");
        }
        if(Input.GetMouseButtonDown(1))
        {
            EventCenter.Instance.Trigger<string>("mains", "good");
        }
    }

    public void Click()
    {
        //GameObject img = Instantiate(Resources.Load<GameObject>("image"));
        //CanvasMgr.Instance.SetParent(img, UI_Layer.BOT);
    }
}
