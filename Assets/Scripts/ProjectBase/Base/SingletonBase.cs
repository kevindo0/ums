using System.Collections;
using System.Collections.Generic;

public class SingletonBase<T> where T: new()
{
    private static T instance;

    // 保证线程安全
    private static object lockobj = new object();

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                lock (lockobj)
                {
                    if (instance == null)
                    {
                        instance = new T();
                    }
                }
            }
            return instance;
        }
    }
}
