using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Entity : MonoBehaviour
{
    private Collider2D _collider2D;
    public Collider2D Collider2D => _collider2D;

    protected Entity(EntityType entityType)
    {
        EntityType = entityType;
        SceneManager.sceneUnloaded += OnSceneUnloaded;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        gameObject.SetActive(true);
    }

    private void OnSceneUnloaded(Scene scene)
    {
        gameObject.SetActive(false);
    }

    public EntityType EntityType { get; }

    protected virtual void OnEnable()
    {
        _collider2D = GetComponent<Collider2D>();
        this.Register();
    }

    protected virtual void OnDisable()
    {
        this.Unregister();
    }
}