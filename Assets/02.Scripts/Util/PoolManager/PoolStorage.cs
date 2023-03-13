using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PoolStorage : MonoBehaviour
{
    private static readonly Dictionary<GameObject, Stack<GameObject>> PooledObjects = new();
    private static readonly Dictionary<GameObject, List<GameObject>> RegisteredObjects = new();

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Init()
    {
        var go = new GameObject("PoolManager");
        go.AddComponent<PoolStorage>();
        go.hideFlags = HideFlags.HideInHierarchy;
        
        SceneManager.sceneUnloaded += OnSceneUnloaded;

        DontDestroyOnLoad(go);
    }

    private static void OnSceneUnloaded(Scene _)
    {
        var keys = new GameObject[PooledObjects.Keys.Count];
        PooledObjects.Keys.CopyTo(keys, 0);
        foreach (var key in keys)
        {
            if (key == null)
            {
                PooledObjects.Remove(key);
            }
        }
        
        var registeredKeys = new GameObject[RegisteredObjects.Keys.Count];
        RegisteredObjects.Keys.CopyTo(registeredKeys, 0);
        foreach (var key in registeredKeys)
        {
            if (key == null)
            {
                RegisteredObjects.Remove(key);
            }
        }
    }

    public static GameObject InstantiateObject(GameObject prefab, Transform parent = null)
    {
        var go = Instantiate(prefab, parent);
        RegisterObject(go, prefab);
        if (go.TryGetComponent(out PoolObject _))
        {
            return go;
        }
        go.AddComponent<PoolObject>().prefab = prefab;
        return go;
    }

    private static void RegisterObject(GameObject go, GameObject prefab, bool isPooled = false)
    {
        if (!RegisteredObjects.ContainsKey(prefab))
        {
            RegisteredObjects.Add(prefab, new List<GameObject>());
        }

        go.name = $"{prefab.name} [{RegisteredObjects[prefab].Count}]";
        RegisteredObjects[prefab].Add(go);

        if (isPooled)
        {
            go.SetActive(false);
        }
    }

    public static GameObject GetObject(GameObject prefab, Transform parent = null)
    {
        if (!PooledObjects.ContainsKey(prefab))
        {
            PooledObjects.Add(prefab, new Stack<GameObject>());
        }

        if (PooledObjects[prefab].Count > 0)
        {
            var go = PooledObjects[prefab].Pop();
            go.transform.SetParent(parent);
            go.SetActive(true);
            return go;
        }

        var instance = InstantiateObject(prefab, parent);
        var poolObject = instance.GetComponent<PoolObject>();
        poolObject.onInit.Invoke();
        instance.hideFlags = HideFlags.None;
        return instance;
    }

    public static void ReturnObject(GameObject instance)
    {
        var poolObject = instance.GetComponent<PoolObject>();
        poolObject.onReturn.Invoke();
        var prefab = poolObject.prefab;
        if (prefab == null)
        {
            Debug.LogError("Prefab not found for instance " + instance.name);
            return;
        }

        if (!PooledObjects.ContainsKey(prefab))
        {
            PooledObjects.Add(prefab, new Stack<GameObject>());
        }

        PooledObjects[prefab].Push(instance);
        instance.SetActive(false);
        instance.hideFlags = HideFlags.HideInHierarchy;
    }
}