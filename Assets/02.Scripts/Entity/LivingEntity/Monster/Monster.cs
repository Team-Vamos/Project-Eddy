using System;
using UnityEngine;
using UnityEngine.AI;

public class Monster : LivingEntity
{
    public delegate void InitDelegate();

    public event InitDelegate OnInit;

    public MonsterStats stats;
    public Entity Target => _target;
    private Entity _target;

    public float AttackTime => _attackTime;
    public bool CanAttack { get; set; }

    public bool CanMove
    {
        get => _navMeshAgent.enabled;
        set => _navMeshAgent.enabled = value;
    }

    private float _attackTime;

    private NavMeshAgent _navMeshAgent;

    private MonsterAttack _monsterAttack;

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

        _attackTime = 0f;
        _target = null;

        Health = stats.health;
        _navMeshAgent.speed = stats.speed;
        _navMeshAgent.stoppingDistance = stats.attackRange - 0.1f;

        _monsterAttack = gameObject.GetComponent<MonsterAttack>();
        _monsterAttack.Init(this);

        CanAttack = true;
        CanMove = true;
    }

    private void SearchTarget()
    {
        var minDistance = float.MaxValue;

        foreach (var entity in EntityManager.Instance.Entities)
        {
            if (entity == this) continue;
            if (entity is not IDamageTaker) continue;
            if (!stats.targetType.HasFlag(entity.EntityType)) continue;
            var distance = Vector2.Distance(transform.position, entity.transform.position);
            if (distance > minDistance) continue;
            _target = entity;
            minDistance = distance;
        }

        if (_target == null) return;
        
        if (_navMeshAgent.isOnNavMesh)
            _navMeshAgent.SetDestination(_target.Collider2D.ClosestPoint(transform.position));
    }

    private void Update()
    {
        SearchTarget();
        if (!CanAttack) return;
        _attackTime += Time.deltaTime;
        if (_target == null) return;
        if (_attackTime < stats.attackSpeed) return;
        TryAttack();
        _attackTime = 0f;
    }

    private void TryAttack()
    {
        var position = transform.position;
        Debug.DrawLine(position, _target.Collider2D.ClosestPoint(position), Color.yellow, 1f);
        if (Vector2.Distance(transform.position, _target.Collider2D.ClosestPoint(transform.position)) >
            stats.attackRange) return;

        Attack();
    }

    private void Attack()
    {
        _navMeshAgent.speed = 0f;
        _monsterAttack.Attack(_target);
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