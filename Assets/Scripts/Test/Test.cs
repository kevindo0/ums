using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    void Start()
    {
        Debug.Log(8888);
        Transform result;

        for (int i = 1; i < 5; i++)
        {
            string sph;

            sph = "s" + i.ToString();
            result = gameObject.transform.Find(sph);

            if (result)
            {
                Debug.Log("Found: " + sph);
            }
            else
            {
                Debug.Log("Did not find: " + sph);
            }
        }
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            //CacheMgr.Instance.Pop("Prefabs/Sprite");
        }
    }
}
