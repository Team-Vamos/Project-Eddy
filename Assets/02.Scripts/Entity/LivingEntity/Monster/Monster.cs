using System;
using UnityEngine;

public class Monster : LivingEntity
{
    public MonsterStats stats;
    public Entity Target => _target;
    private Entity _target;

    private float _attackTime;

    public void Init()
    {
        if (stats is null)
        {
            Debug.LogWarning("Monster stats is null");
            return;
        }

        Health = stats.health;
    }

    private void SearchTarget()
    {
        var minDistance = float.MaxValue;

        foreach (var entity in EntityManager.Instance.Entities)
        {
            if (entity == this) continue;
            if (entity is not IDamageTaker damageTaker) continue;
            if (!stats.targetType.HasFlag(entity.EntityType)) continue;
            var distance = Vector3.Distance(transform.position, entity.transform.position);
            if (distance > minDistance) continue;
            _target = entity;
            minDistance = distance;
        }
    }

    private void Update()
    {
        _attackTime += Time.deltaTime;
        SearchTarget();
        if (_target == null) return;
        if (_attackTime < stats.attackSpeed) return;
        TryAttack();
        _attackTime = 0f;
    }

    private void TryAttack()
    {
        if (Vector3.Distance(transform.position, _target.Collider2D.ClosestPoint(transform.position)) >
            stats.attackRange) return;
        Attack();
        Debug.Log("Attack");
    }

    private void Attack()
    {
        if (stats.attackType == AttackType.Area)
        {
            foreach (var entity in EntityManager.Instance.Entities)
            {
                if (entity == this) continue;
                if (entity is not IDamageTaker damageTaker) continue;
                if (stats.targetType.HasFlag(EntityType.Monster)) continue;
                if (Vector3.Distance(transform.position, entity.Collider2D.ClosestPoint(transform.position)) >
                    stats.attackRange) continue;
                damageTaker.TakeDamage(stats.damage);
            }
        }
        else
        {
            (_target as IDamageTaker)?.TakeDamage(stats.damage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, stats.attackRange);

        if (_target != null)
        {
            Gizmos.color = Color.green;
            var position = transform.position;
            Gizmos.DrawLine(position, _target.Collider2D.ClosestPoint(position));
        }

        if (stats.attackType == AttackType.Area)
        {
            Gizmos.color = Color.red;
        }
    }

    protected Monster() : base(EntityType.Monster)
    {
    }
}