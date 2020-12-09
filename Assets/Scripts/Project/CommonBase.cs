using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonBase
{
    public static string StreamingAssetsPath(string path)
    {
        string newpath =
#if UNITY_ANDROID && !UNITY_EDITOR
        Application.streamingAssetsPath;
#elif UNITY_IPHONE && !UNITY_EDITOR
        "file://" + Application.streamingAssetsPath ;
#elif UNITY_STANDLONE_WIN||UNITY_EDITOR
        "file://" + Application.streamingAssetsPath;
#else
        string.Empty;
#endif
        return newpath + "/" + path;
    }
}
