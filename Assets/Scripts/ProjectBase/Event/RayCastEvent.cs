using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastEvent : MonoBehaviour
{
    Vector2 ray;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, ray, Color.red);
        if (Input.GetMouseButtonDown(0))
        {
            ray = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, ray);
            //Debug.Log("hit:" + hit.collider);
            if (hit.collider != null)
            {
                Debug.Log("Target Position: " + hit.collider.gameObject.name);
            }
            else
            {
                Debug.Log("not found");
            }
        }
    }
}
