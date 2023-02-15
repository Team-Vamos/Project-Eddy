using System.Collections.Generic;
using UnityEngine;

public class EntityManager : MonoBehaviour
{
    public static EntityManager Instance { get; private set; }
    public IReadOnlyList<Entity> Entities => _entities;
    
    private readonly List<Entity> _entities = new();

    private void Awake()
    {
        Instance = this;
    }

    public void RegisterEntity(Entity entity)
    {
        _entities.Add(entity);
    }

    public void UnregisterEntity(Entity entity)
    {
        _entities.Remove(entity);
    }
}

public static class EntityExtensions
{
    public static void Register(this Entity entity)
    {
        EntityManager.Instance.RegisterEntity(entity);
    }
    
    public static void Unregister(this Entity entity)
    {
        EntityManager.Instance.UnregisterEntity(entity);
    }

    public static void Damage<T>(this T entity, int damage) where T : Entity, IDamageTaker
    {
        entity.TakeDamage(damage);
    }

    public static void GiveExp<T>(this T entity, int exp) where T : Entity, IExpGiver
    {
        entity.GiveExp(exp);
    }
}