using Mirror;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Entity : NetworkBehaviour
{
    public Collider2D Collider2D { get; private set; }

    protected Entity(EntityType entityType)
    {
        EntityType = entityType;
        SceneManager.sceneUnloaded += OnSceneUnloaded;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        try
        {
            gameObject.SetActive(true);
        }
        catch
        {
            // ignored
        }
    }

    private void OnSceneUnloaded(Scene scene)
    {
        try
        {
            gameObject.SetActive(false);
        }
        catch
        {
            // ignored
        }
    }

    [field: SyncVar] public EntityType EntityType { get; }

    protected virtual void OnEnable()
    {
        Collider2D = GetComponent<Collider2D>();
        this.Register();
    }

    protected virtual void OnDisable()
    {
        this.Unregister();
    }
}