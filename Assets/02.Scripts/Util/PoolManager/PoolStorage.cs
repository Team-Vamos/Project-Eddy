using System.Collections.Generic;
using UnityEngine;

public class PoolStorage : MonoBehaviour
{
    private static readonly Dictionary<GameObject, Stack<GameObject>> PooledObjects = new();
    private static readonly Dictionary<GameObject, List<GameObject>> RegisteredObjects = new();

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Init()
    {
        var go = new GameObject("PoolManager");
        go.AddComponent<PoolManager>();
        go.hideFlags = HideFlags.HideInHierarchy;

        DontDestroyOnLoad(go);
    }

    public static GameObject InstantiateObject(GameObject prefab, Transform parent = null)
    {
        var go = Instantiate(prefab, parent);
        go.AddComponent<PoolObject>().prefab = prefab;
        RegisterObject(go, prefab);
        return go;
    }

    private static void RegisterObject(GameObject go, GameObject prefab, bool isPooled = false)
    {
        if (!RegisteredObjects.ContainsKey(prefab))
        {
            RegisteredObjects.Add(prefab, new List<GameObject>());
        }

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
        return instance;
    }

    public static void ReturnObject(GameObject instance)
    {
        var prefab = GetPrefab(instance);
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
    }

    private static GameObject GetPrefab(GameObject instance)
    {
        var poolObject = instance.GetComponent<PoolObject>();
        return poolObject != null ? poolObject.prefab : null;
    }
}