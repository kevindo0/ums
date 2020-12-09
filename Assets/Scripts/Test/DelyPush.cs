using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelyPush : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Push", 1);
    }

    // Update is called once per frame
    void Push()
    {
        Debug.Log("push");
        CacheMgr.Instance.Push(gameObject);
    }
}
