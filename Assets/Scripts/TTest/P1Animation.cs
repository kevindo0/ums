using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P1Animation : MonoBehaviour
{
    private Animator _animation;

    void Start()
    {
        _animation = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            _animation.SetFloat("Blend", 0.5f);
        } else if (Input.GetKey(KeyCode.D))
        {
            _animation.SetFloat("Blend", 1.0f);
        }
        else if (Input.GetKey(KeyCode.W))
        {
            _animation.SetFloat("Blend", 1.5f);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            _animation.SetFloat("Blend", 0.0f);
        }
    }
}
