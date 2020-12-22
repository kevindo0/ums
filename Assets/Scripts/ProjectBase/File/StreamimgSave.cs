using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;


/// <summary>
/// StreamingAssets 中的配置文件全部存放在文件夹下 Config 文件夹下
/// </summary>
public class StreamimgSave
{
    private string _fileName;

    public StreamimgSave(string fileName)
    {
        _fileName = fileName;
    }

    // 获取 streamingAssets 路径
    private string GetPath()
    {
        return Path.Combine(Application.streamingAssetsPath, "Config", _fileName);
    }

    public T LoadbyJson<T>()
    {
        string fileText = null;
        if (Application.platform == RuntimePlatform.Android)
            fileText = LoadFromAndroidSync();
        else
            fileText = LoadFromOtherSync();
        return JsonUtility.FromJson<T>(fileText);
    }

    // 安卓同步读取文件方法
    private string LoadFromAndroidSync()
    {
        Dictionary<string, string> res = new Dictionary<string, string>();
        IEnumerator e = LoadFromAndroid(res);
        while (e.MoveNext()) ;
        if (res.ContainsKey("res"))
            return res["res"];
        return null;
    }

    IEnumerator LoadFromAndroid(Dictionary<string, string> res)
    {
        string path = GetPath();
        path = "www.baidu.com";
        UnityWebRequest uwr = UnityWebRequest.Get(path);
        yield return uwr.SendWebRequest();

        // 如果未加载完成 返回true,需要继续等待
        while (!uwr.isDone)
        {
            yield return true;
        }
        // 读取文件出错时不返回任何内容
        if (uwr.isHttpError || uwr.isNetworkError)
            Debug.Log("android load error:" + uwr.error);
        else
            res.Add("res", uwr.downloadHandler.text);
    }

    private string LoadFromOtherSync()
    {
        string filePath = GetPath();
        if (File.Exists(filePath))
        {
            StreamReader sr = new StreamReader(filePath);
            // 将获取到的流赋值给fileText
            string fileText = sr.ReadToEnd();
            sr.Close();
            return fileText;
        }
        return null;
    }
}
