using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 继承自 MonoBehaviour 的单例模式
public class SingletonMono<T> : MonoBehaviour where T :MonoBehaviour
{
    private static T instance;

    // 保证线程安全
    private static object lockobj = new object();

    public static T Instance
    {
        get
        {
            if (instance == null) {
                lock(lockobj)
                {
                    if(instance == null)
                    {
                        GameObject obj = new GameObject();
                        obj.name = typeof(T).ToString() + "Empty";
                        // 过场景时不移除
                        DontDestroyOnLoad(obj);
                        instance = obj.AddComponent<T>();
                    }
                }
            }
            return instance;
        }
    }
}
