using UnityEngine;

public class PoolManager : MonoBehaviour
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Init()
    {
        var go = new GameObject("PoolManager");
        go.AddComponent<PoolManager>();
        go.hideFlags = HideFlags.HideInHierarchy;

        DontDestroyOnLoad(go);
    }

    public static GameObject GetObject(GameObject prefab)
    {
        return PoolStorage.GetObject(prefab);
    }

    public void PreloadObject(GameObject prefab, int count)
    {
        for (int i = 0; i < count; i++)
        {
            PoolStorage.InstantiateObject(prefab);
        }
    }

    public new static GameObject Instantiate(GameObject prefab, Transform parent = null)
    {
        return PoolStorage.GetObject(prefab, parent);
    }

    public static void Destroy(GameObject instance)
    {
        PoolStorage.ReturnObject(instance);
    }

    public static PoolManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }
}