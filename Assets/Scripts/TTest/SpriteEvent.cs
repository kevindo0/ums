using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteEvent : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if (UGUICheck.Instance.CheckGuiRaycastObject()) return;
        Debug.Log("Sprite Clicked");
    }
}
