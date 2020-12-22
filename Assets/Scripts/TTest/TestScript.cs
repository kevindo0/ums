using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameFileMgr m = new GameFileMgr("player.json");
        PlayerData data = m.Decode<PlayerData>();
        Debug.Log("data:" + data.name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
