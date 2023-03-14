using Mirror;
using UnityEngine;

public class NetworkSingleton<T> : NetworkBehaviour where T : NetworkSingleton<T>
{
    private static bool _shuttingDown = false;
    private static object _locker = new();
    private static T _instance = null;

    public static T Instance
    {
        get
        {
            if (_shuttingDown)
            {
                Debug.LogWarning("[Instance] Instance" + typeof(T) + "is already destroyed. Returning null.");
                return null;
            }

            lock (_locker)
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<T>();
                    if (_instance == null)
                    {
                        var instanceObject = new GameObject(typeof(T).ToString());
                        instanceObject.AddComponent<NetworkIdentity>();
                        _instance = instanceObject.AddComponent<T>();
                        DontDestroyOnLoad(_instance);
                    }
                }

                return _instance;
            }
        }
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject.transform.root);
        _shuttingDown = false;
    }

    private void OnDestroy()
    {
        _shuttingDown = true;
    }

    private void OnApplicationQuit()
    {
        _shuttingDown = true;
    }
}