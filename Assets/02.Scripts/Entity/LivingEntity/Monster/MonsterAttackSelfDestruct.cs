using System.Collections;
using UnityEngine;

public class MonsterAttackSelfDestruct : MonsterAttack
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
        yield return new WaitForSeconds(explosionTime);
        Instantiate(explosionEffect, Monster.transform.position, Quaternion.identity);
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

        Destroy(Monster.gameObject);
    }
}