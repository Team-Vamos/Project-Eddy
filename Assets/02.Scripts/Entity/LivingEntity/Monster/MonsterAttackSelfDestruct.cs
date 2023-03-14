using System.Collections;
using DG.Tweening;
using UnityEngine;

public class MonsterAttackSelfDestruct : MonsterAttack, IPoolable
{
    [SerializeField] private float explosionTime;
    [SerializeField] private float explosionRadius;
    [SerializeField] private GameObject explosionEffect;

    public override void Attack(Entity _)
    {
        Monster.CanAttack = false;
        Monster.CanMove = false;
        StartCoroutine(SelfDestruct());
    }
 
    private IEnumerator SelfDestruct()
    {
        foreach (var monsterRenderer in Monster.Renderers)
            monsterRenderer.material.DOColor(Color.red, explosionTime).SetEase(Ease.Linear);
        yield return new WaitForSeconds(explosionTime);
        PoolManager.Instantiate(explosionEffect, Monster.transform.position, Quaternion.identity);
        foreach (var entity in EntityManager.Instance.Entities)
        {
            if (entity == Monster) continue;
            if (entity is not IDamageTaker damageTaker) continue;
            if (entity.EntityType.HasFlag(EntityType.Monster)) continue;
            if (Vector2.Distance(Monster.transform.position,
                    entity.Collider2D.ClosestPoint(Monster.transform.position)) >
                explosionRadius) continue;
            damageTaker.TakeDamage(Monster.stats.damage);
        }

        Monster.Init();
        PoolManager.Destroy(Monster.gameObject);
    }

    public void OnInit()
    {
        StartCoroutine(Initialize());
    }
    
    private IEnumerator Initialize()
    {
        yield return null;
        foreach (var monsterRenderer in Monster.Renderers)
            monsterRenderer.material.DOColor(Color.white, 0.1f).SetEase(Ease.Linear);
    }

    public void OnReturn()
    {
        
    }
}