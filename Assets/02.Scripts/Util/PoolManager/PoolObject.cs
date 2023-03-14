using UnityEngine;
using UnityEngine.Events;

public class PoolObject : MonoBehaviour
{
    public GameObject prefab;
    public UnityEvent onInit = new();
    public UnityEvent onReturn = new();

    private bool _isInit;

    public void Init()
    {
        if (_isInit) return;
        foreach (var component in GetComponentsInChildren<MonoBehaviour>())
            if (component is IPoolable poolable)
            {
                onInit.AddListener(poolable.OnInit);
                onReturn.AddListener(poolable.OnReturn);
            }

        _isInit = true;
    }

    private void Awake()
    {
        Init();
    }
}