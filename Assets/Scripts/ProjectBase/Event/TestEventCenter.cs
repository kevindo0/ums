using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// EventCenter.Instance.Trigger("main");
// EventCenter.Instance.Trigger<string>("mains", "good");

public class TestEventCenter : MonoBehaviour
{
    private void Start()
    {
        EventCenter.Instance.AddEventListener("main", Update1);
        EventCenter.Instance.AddEventListener("main", Update2);

        EventCenter.Instance.AddEventListener<string>("mains", Update3);
        EventCenter.Instance.AddEventListener<string>("mains", Update4);
    }

    private void OnDestroy()
    {
        EventCenter.Instance.RemoveEventListener("main", Update1);
        EventCenter.Instance.RemoveEventListener("main", Update2);

        EventCenter.Instance.RemoveEventListener<string>("mains", Update3);
        EventCenter.Instance.RemoveEventListener<string>("mains", Update4);
    }

    public void Update1()
    {
        Debug.Log("update 1");
    }

    public void Update2()
    {
        Debug.Log("update 2");
    }

    public void Update3(string text)
    {
        Debug.Log("update 3 text: " + text);
    }

    public void Update4(string text)
    {
        Debug.Log("update 4 text: " + text);
    }
}
