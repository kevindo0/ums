using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using LitJson;

public class JsonFileMgr<T> where T: new() {
    private string fileName;

    public JsonFileMgr(string fileName)
    {
        this.fileName = fileName;
    }

    public T Decode()
    {
        T res = new T();
        try
        {
            using (StreamReader r = new StreamReader(Application.streamingAssetsPath + "/" + fileName))
            {
                string s = r.ReadToEnd();
                res = JsonMapper.ToObject<T>(s);
            }
        }
        catch(Exception e)
        {
            Debug.Log(e.Message);
        }
        return res;
    }

    public void Encode()
    {
        Debug.Log("Encode");
    }
}
