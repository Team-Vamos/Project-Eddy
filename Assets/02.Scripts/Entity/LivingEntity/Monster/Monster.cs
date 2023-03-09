using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class Monster : LivingEntity
{
    public MonsterStats stats;
    public Entity Target => _target;
    private Entity _target;

    public float AttackTime => _attackTime;
    private float _attackTime;

    private NavMeshAgent _navMeshAgent;

    private MonsterRangeAttack _rangeAttack;

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.updateUpAxis = false;
        _navMeshAgent.updateRotation = false;
    }

    public void Init()
    {
        if (stats is null)
        {
            Debug.LogWarning("Monster stats is null");
            return;
        }

        Health = stats.health;
        _navMeshAgent.speed = stats.speed;
        _navMeshAgent.stoppingDistance = stats.attackRange - 0.1f;

        if (stats.damageType == DamageType.Ranged)
        {
            _rangeAttack = gameObject.GetComponent<MonsterRangeAttack>();
            _rangeAttack.Init(this);
        }
    }

    private void SearchTarget()
    {
        var minDistance = float.MaxValue;

        foreach (var entity in EntityManager.Instance.Entities)
        {
            if (entity == this) continue;
            if (entity is not IDamageTaker damageTaker) continue;
            if (!stats.targetType.HasFlag(entity.EntityType)) continue;
            var distance = Vector2.Distance(transform.position, entity.transform.position);
            if (distance > minDistance) continue;
            _target = entity;
            minDistance = distance;
        }

        if (_target != null)
        {
            _navMeshAgent.SetDestination(_target.Collider2D.ClosestPoint(transform.position));
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
        Debug.DrawLine(transform.position, _target.Collider2D.ClosestPoint(transform.position), Color.yellow, 1f);
        if (Vector2.Distance(transform.position, _target.Collider2D.ClosestPoint(transform.position)) >
            stats.attackRange) return;

        Attack();
    }

    private void Attack()
    {
        _navMeshAgent.speed = 0f;

        if (stats.damageType == DamageType.Ranged)
        {
            _rangeAttack.Attack(_target.Collider2D.ClosestPoint(transform.position));
            return;
        }

        if (stats.attackType == AttackType.Area)
        {
            foreach (var entity in EntityManager.Instance.Entities)
            {
                if (entity == this) continue;
                if (entity is not IDamageTaker damageTaker) continue;
                if (entity.EntityType.HasFlag(EntityType.Monster)) continue;
                if (Vector2.Distance(transform.position, _target.Collider2D.ClosestPoint(transform.position)) >
                    stats.attackRange) continue;
                damageTaker.TakeDamage(stats.damage);
            }
        }
        else
        {
            (_target as IDamageTaker)?.TakeDamage(stats.damage);
        }

        _navMeshAgent.speed = 1f;
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

            if (Vector2.Distance(position, _target.Collider2D.ClosestPoint(position)) < stats.attackRange)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawLine(position, _target.Collider2D.ClosestPoint(position));
            }
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