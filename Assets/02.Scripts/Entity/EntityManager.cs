using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EntityManager : MonoBehaviour
{
    #region static methods
    public static EntityManager Instance { get; private set; }

    [RuntimeInitializeOnLoadMethod]
    private static void Initialize()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private static void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (Instance != null || mode != LoadSceneMode.Single) return;
        
        var go = new GameObject("EntityManager");
        Instance = go.AddComponent<EntityManager>();
    }
    #endregion
    
    public IReadOnlyList<Entity> Entities => _entities;
    
    private readonly List<Entity> _entities = new();

    private void Awake()
    {
        Instance = this;
        gameObject.hideFlags = HideFlags.HideAndDontSave;
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

    public static void Damage<T>(this T entity, float damage) where T : Entity, IDamageTaker
    {
        entity.TakeDamage(damage);
    }

    public static void GiveExp<T>(this T entity, int exp) where T : Entity, IExpTaker
    {
        entity.TakeExp(exp);
    }
}