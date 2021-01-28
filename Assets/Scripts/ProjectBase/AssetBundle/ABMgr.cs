using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// GameObject obj = ABMgr.Instance.LoadRes("model", "Cube") as GameObject;
// GameObject obj = ABMgr.Instance.LoadRes("model", "Cube", typeof(Gameobject)) as GameObject;
// GameObject obj = ABMgr.Instance.LoadRes<GameObject>("model", "Cube");
// obj.transform.position = Vector3.up;

// ABMgr.Instance.LoadResAsync("model", "Nt", (obj) =>
// {
//     (obj as GameObject).transform.position = Vector3.up;
// });
// ABMgr.Instance.LoadResAsync<GameObject>("model", "Nt", (obj) =>
// {
//     obj.transform.position = Vector3.up;
// });

/// <summary>
/// AssetBundle 资源管理
/// </summary>
public class ABMgr : SingletonMono<ABMgr>
{
    // 主包
    private AssetBundle mainAB = null;
    // 依赖包文件
    private AssetBundleManifest manifest = null;
    // 存储加载的AB包
    private Dictionary<string, AssetBundle> abDic = new Dictionary<string, AssetBundle>();

    // AB包存放路径
    // 热更新时路径为 Application.persistentDataPath
    private string PathUrl
    {
        get
        {
            return Application.streamingAssetsPath + "/";
        }
    }

    //
    private string MainABName
    {
        get
        {
#if UNITY_IOS
    return "IOS";
#elif UNITY_ANDROID
    return "Android";
#else
     return "PC";
#endif
        }
    }

    // 加载AB包 及其依赖
    public void LoadAB(string abName)
    {
        if (mainAB == null)
        {
            mainAB = AssetBundle.LoadFromFile(PathUrl + MainABName);
            manifest = mainAB.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
        }
        string[] strs = manifest.GetDirectDependencies(abName);
        AssetBundle ab = null;
        for (int i = 0; i < strs.Length; i++)
        {
            if (!abDic.ContainsKey(strs[i]))
            {
                ab = AssetBundle.LoadFromFile(PathUrl + strs[i]);
                abDic.Add(strs[i], ab);
            }
        }
        if (!abDic.ContainsKey(abName))
        {
            ab = AssetBundle.LoadFromFile(PathUrl + abName);
            abDic.Add(abName, ab);
        }
    }

    // 同步加载
    public Object LoadRes(string abName, string resName)
    {
        LoadAB(abName);
        Object obj = abDic[abName].LoadAsset(resName);
        if (obj is GameObject)
            return Instantiate(obj);
        else
            return obj;
    }

    // 同步加载 根据type指定类型
    public Object LoadRes(string abName, string resName, System.Type type)
    {
        LoadAB(abName);
        Object obj = abDic[abName].LoadAsset(resName, type);
        if (obj is GameObject)
            return Instantiate(obj);
        else
            return obj;
    }

    // 同步加载 根据泛型指定类型
    public T LoadRes<T>(string abName, string resName) where T : Object
    {
        LoadAB(abName);
        T obj = abDic[abName].LoadAsset<T>(resName);
        if (obj is GameObject)
            return Instantiate(obj);
        else
            return obj;
    }

    // AB包加载未使用异步
    // 从AB包加载资源时使用异步
    public void LoadResAsync(string abName, string resName, UnityAction<Object> callBack)
    {
        StartCoroutine(ILoadRes(abName, resName, callBack));
    }

    private IEnumerator ILoadRes(string abName, string resName, UnityAction<Object> callBack)
    {
        LoadAB(abName);
        AssetBundleRequest abr = abDic[abName].LoadAssetAsync(resName);
        yield return abr;
        // 异步加载后， 通过委托 传递给外部
        if (abr.asset is GameObject)
            callBack(Instantiate(abr.asset));
        else
            callBack(abr.asset);
    }

    // 根据type异步加载资源
    public void LoadResAsync(string abName, string resName, System.Type type, UnityAction<Object> callBack)
    {
        StartCoroutine(ILoadRes(abName, resName, type, callBack));
    }

    private IEnumerator ILoadRes(string abName, string resName, System.Type type, UnityAction<Object> callBack)
    {
        LoadAB(abName);
        AssetBundleRequest abr = abDic[abName].LoadAssetAsync(resName, type);
        yield return abr;
        if (abr.asset is GameObject)
            callBack(Instantiate(abr.asset));
        else
            callBack(abr.asset);
    }

    // 根据泛型异步加载资源
    public void LoadResAsync<T>(string abName, string resName, UnityAction<T> callBack) where T : Object
    {
        StartCoroutine(ILoadRes<T>(abName, resName, callBack));
    }

    private IEnumerator ILoadRes<T>(string abName, string resName, UnityAction<T> callBack) where T : Object
    {
        LoadAB(abName);
        AssetBundleRequest abr = abDic[abName].LoadAssetAsync<T>(resName);
        yield return abr;
        if (abr.asset is GameObject)
            callBack(Instantiate(abr.asset) as T);
        else
            callBack(abr.asset as T);
    }

    // 单个包卸载
    public void Unload(string abName)
    {
        if(abDic.ContainsKey(abName))
        {
            abDic[abName].Unload(false);
            abDic.Remove(abName);
        }
    }

    // 卸载所有的包
    public void UnloadAll()
    {
        AssetBundle.UnloadAllAssetBundles(false);
        abDic.Clear();
        mainAB = null;
        manifest = null;
    }
}
