using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonTest : SingletonMono<SingletonTest>
{
    void Start()
    {

    }

    void Update()
    {
        
    }

    public void GameOver()
    {
        Debug.Log("game over");
    }
}
