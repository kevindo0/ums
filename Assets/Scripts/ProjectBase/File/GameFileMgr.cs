using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


/// <summary>
/// 游戏文件管理
/// 配置文件存放在 StreamingAssets 文件夹中，使用时复制到 Persistent 文件夹下
/// </summary>
public class GameFileMgr
{
    private string _fileName;

    public GameFileMgr(string fileName)
    {
        this._fileName = fileName;
    }

    public T Decode<T>()
    {
        string path = Path.Combine(Application.persistentDataPath, _fileName);
        PersistentSave ps = new PersistentSave(_fileName);

        if (!File.Exists(path))
        {
            StreamimgSave ss = new StreamimgSave(_fileName);
            T data = ss.LoadbyJson<T>();
            // 未处理配置文件不存在的情况
            ps.SaveByBin<T>(data);
        }

        return ps.LoadByBin<T>();
    }
    // 只是读取配置文件，不修改
    public T DecodeOnly<T>()
    {
        string path = Path.Combine(Application.persistentDataPath, _fileName);
        StreamimgSave ss = new StreamimgSave(_fileName);
        return ss.LoadbyJson<T>();
    }

    // 数据保存
    public bool Encode<T>(T s)
    {
        PersistentSave ps = new PersistentSave(_fileName);
        return ps.SaveByBin<T>(s);
    }
}

/// Usage:
/// GameFileMgr m = new GameFileMgr("player.json");
/// PlayerData data = m.Decode<PlayerData>();
/// Debug.Log("data:" + data.name);
