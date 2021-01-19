using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashingSwitch : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    private Material material;
    private float switchCount = 0;

    void Start()
    {
        material = spriteRenderer.material;
    }

    private void FixedUpdate()
    {
        //material.SetFloat("_Switch", switchCount);
        switchCount += Time.deltaTime;
        if(switchCount > 5)
        {
            switchCount = 0;
        }
        if((switchCount > 3 && switchCount <= 3.5) || (switchCount > 4 && switchCount <= 4.5))
        {
            material.SetFloat("_Switch", 1);
        } else if ((switchCount > 3.5 && switchCount <= 4) || (switchCount > 4.5 && switchCount <= 5))
        {
            material.SetFloat("_Switch", 2);
        } else
        {
            material.SetFloat("_Switch", 0);
        }
    }
}
