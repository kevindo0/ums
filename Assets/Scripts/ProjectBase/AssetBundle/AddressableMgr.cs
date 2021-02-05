using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using System.Threading.Tasks;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AddressableMgr
{
    public static async Task<T> LoadAsset<T>(string key) where T : Object
    {
        AsyncOperationHandle<T> handle = Addressables.LoadAssetAsync<T>(key);
        await handle.Task;
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            return handle.Result;
        }
        return null;
    }

    public static async Task<T> LoadAsset<T>(string key, System.Action action) where T : Object
    {
        AsyncOperationHandle<T> handle = Addressables.LoadAssetAsync<T>(key);
        await handle.Task;
        action.Invoke();
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            action.Invoke();
            return handle.Result;
        }
        return null;
    }

    public static async Task<GameObject> Instantiate(string key)
    {
        AsyncOperationHandle<GameObject> handle = Addressables.InstantiateAsync(key);
        await handle.Task;
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            return handle.Result;
        }
        return null;
    }

    public static async Task<GameObject> Instantiate(string key, System.Action action)
    {
        AsyncOperationHandle<GameObject> handle = Addressables.InstantiateAsync(key);
        await handle.Task;
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            action.Invoke();
            return handle.Result;
        }
        return null;
    }
}
