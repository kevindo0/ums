using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[System.Serializable]
public class Save
{
    public int id;
    public string name;

    public Save(int id, string name)
    {
        this.id = id;
        this.name = name;
    }
}

/// <summary>
/// 数据持久化
/// </summary>
public class FileSave
{
    protected string fileName;

    public FileSave(string fileName)
    {
        this.fileName = fileName;
    }

    public virtual string GetPath()
    {
        return fileName;
    }

    // 二进制文件 读档
    public T LoadByBin<T>()
    {
        T res = default(T);
        string path = GetPath();
        if (File.Exists(path))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = File.Open(path, FileMode.Open);
            try
            {
                res = (T)bf.Deserialize(fs);
            }
            catch (System.Exception e)
            {
                Debug.Log("open error:" + e);
            }
            finally
            {
                fs.Close();
            }
        }
        return res;
    }

    // 二进制文件 存档
    public bool SaveByBin<T>(T s)
    {
        bool res = false;
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = File.Create(GetPath());
        try
        {
            bf.Serialize(fs, s);
            res = true;
        }
        catch (System.Exception e)
        {
            Debug.Log("Error:" + e);
        }
        finally
        {
            fs.Close();
        }
        return res;
    }

    // Json 读档
    public T LoadByJson<T>()
    {
        T res = default(T);
        string filePath = GetPath();
        if(File.Exists(filePath))
        {
            StreamReader sr = new StreamReader(filePath);
            // 将获取到的流赋值给fileText
            string fileText = sr.ReadToEnd();
            sr.Close();
            res = JsonUtility.FromJson<T>(fileText);
        }
        return res;
    }

    // Json 存档
    public bool SaveByJson<T>(T s)
    {
        string saveText = JsonUtility.ToJson(s);
        return SaveByString(saveText);
    }

    // 字符串 string 存档
    public bool SaveByString(string saveText)
    {
        bool res = false;
        StreamWriter sw = new StreamWriter(GetPath());
        try
        {
            sw.Write(saveText);
            res = true;
        }
        catch (System.Exception e)
        {
            Debug.Log("write string Error:" + e);
        }
        finally
        {
            sw.Close();
        }
        return res;
    }
}
