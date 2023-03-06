using System;
using UnityEngine;

public class Entity : MonoBehaviour
{
    private Collider2D _collider2D;
    public Collider2D Collider2D => _collider2D;

    protected Entity(EntityType entityType)
    {
        EntityType = entityType;
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