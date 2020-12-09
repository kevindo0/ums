using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //SingletonTest.Instance.GameOver();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            //CacheMgr.Instance.Pop("Prefabs/Sprite");
        }
    }

    public void PlayTest()
    {
        Debug.Log("Play test");
    }
}
