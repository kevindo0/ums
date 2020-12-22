using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// unity 持久化数据操作
/// </summary>
public class PersistentSave : FileSave
{

    public PersistentSave(string fileName) : base(fileName)
    {

    }

    public override string GetPath()
    {
        return Application.persistentDataPath + "/" + this.fileName;
    }
}
