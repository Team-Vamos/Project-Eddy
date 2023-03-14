using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PoolStorage : MonoBehaviour
{
    private static readonly Dictionary<GameObject, Stack<GameObject>> PooledObjects = new();
    private static readonly Dictionary<GameObject, List<GameObject>> RegisteredObjects = new();
    private static readonly Dictionary<GameObject, PoolObject> PoolObjects = new();

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
            if (key == null)
                PooledObjects.Remove(key);

        var registeredKeys = new GameObject[RegisteredObjects.Keys.Count];
        RegisteredObjects.Keys.CopyTo(registeredKeys, 0);
        foreach (var key in registeredKeys)
            if (key == null)
                RegisteredObjects.Remove(key);
    }

    public static GameObject InstantiateObject(GameObject prefab, Transform parent = null)
    {
        var instance = Instantiate(prefab, parent);
        RegisterObject(instance, prefab);
        if (instance.TryGetComponent(out PoolObject _)) return instance;
        var poolObject = instance.AddComponent<PoolObject>();
        poolObject.prefab = prefab;
        poolObject.Init();
        PoolObjects.TryAdd(instance, poolObject);
        return instance;
    }

    private static void RegisterObject(GameObject go, GameObject prefab, bool isPooled = false)
    {
        if (!RegisteredObjects.ContainsKey(prefab)) RegisteredObjects.Add(prefab, new List<GameObject>());

        go.name = $"{prefab.name} [{RegisteredObjects[prefab].Count}]";
        RegisteredObjects[prefab].Add(go);

        if (isPooled) go.SetActive(false);
    }

    public static GameObject GetObject(GameObject prefab, Transform parent = null)
    {
        if (!PooledObjects.ContainsKey(prefab)) PooledObjects.Add(prefab, new Stack<GameObject>());

        if (PooledObjects[prefab].Count > 0)
        {
            var go = PooledObjects[prefab].Pop();
            go.transform.SetParent(parent);
            go.SetActive(true);
            return go;
        }

        var instance = InstantiateObject(prefab, parent);
        var poolObject = PoolObjects[instance];
        poolObject.onInit.Invoke();
        instance.hideFlags = HideFlags.None;
        return instance;
    }

    public static void ReturnObject(GameObject instance)
    {
        if (PoolObjects.TryGetValue(instance, out var poolObject))
        {
            poolObject.onReturn.Invoke();
        }
        else
        {
            Debug.LogWarning("Prefab not found for instance " + instance.name);
            Destroy(instance);
            return;
        }

        var prefab = poolObject.prefab;
        if (!PooledObjects.ContainsKey(prefab)) PooledObjects.Add(prefab, new Stack<GameObject>());

        PooledObjects[prefab].Push(instance);
        instance.SetActive(false);
        instance.hideFlags = HideFlags.HideInHierarchy;
    }
}