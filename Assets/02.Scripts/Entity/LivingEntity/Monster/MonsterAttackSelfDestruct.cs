using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class MonsterAttackSelfDestruct : MonsterAttack
{
    [SerializeField] private float explosionTime;
    [SerializeField] private float explosionRadius;
    [SerializeField] private GameObject explosionEffect;

    private void Awake()
    {
        OnInit += () =>
        {
            foreach (var monsterRenderer in Monster.Renderers)
            {
                monsterRenderer.material.color = Color.white;
            }
        };
    }

    public override void Attack(Entity _)
    {
        Monster.CanAttack = false;
        Monster.CanMove = false;
        StartCoroutine(SelfDestruct());
    }

    private IEnumerator SelfDestruct()
    {
        foreach (var monsterRenderer in Monster.Renderers)
        {
            monsterRenderer.material.DOColor(Color.red, explosionTime).SetEase(Ease.Linear);
        }
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
        Monster.Init();
        Destroy(Monster.gameObject);
    }
}