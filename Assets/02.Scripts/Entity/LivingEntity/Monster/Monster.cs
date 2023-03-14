using UnityEngine;
using UnityEngine.AI;

public class Monster : LivingEntity, IPoolable
{
    public delegate void InitDelegate();

    public MonsterStats stats;

    private MonsterAttack _monsterAttack;

    private NavMeshAgent _navMeshAgent;

    protected Monster() : base(EntityType.Monster)
    {
    }

    public Entity Target { get; private set; }

    public float AttackTime { get; private set; }

    public bool CanAttack { get; set; }

    public bool CanMove
    {
        get => _navMeshAgent.enabled;
        set => _navMeshAgent.enabled = value;
    }

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.updateUpAxis = false;
        _navMeshAgent.updateRotation = false;
    }

    private void Update()
    {
        SearchTarget();
        if (!CanAttack) return;
        AttackTime += Time.deltaTime;
        if (Target == null) return;
        if (AttackTime < stats.attackSpeed) return;
        TryAttack();
        AttackTime = 0f;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, stats.attackRange);

        if (Target != null)
        {
            Gizmos.color = Color.green;
            var position = transform.position;
            Gizmos.DrawLine(position, Target.Collider2D.ClosestPoint(position));

            if (Vector2.Distance(position, Target.Collider2D.ClosestPoint(position)) < stats.attackRange)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawLine(position, Target.Collider2D.ClosestPoint(position));
            }
        }

        if (stats.attackType == AttackType.Area) Gizmos.color = Color.red;
    }

    public void Init()
    {
        if (stats is null)
        {
            Debug.LogWarning("Monster stats is null");
            return;
        }

        AttackTime = 0f;
        Target = null;

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
            Target = entity;
            minDistance = distance;
        }

        if (Target == null) return;

        if (_navMeshAgent.isOnNavMesh)
            _navMeshAgent.SetDestination(Target.Collider2D.ClosestPoint(transform.position));
    }

    private void TryAttack()
    {
        var position = transform.position;
        Debug.DrawLine(position, Target.Collider2D.ClosestPoint(position), Color.yellow, 1f);
        if (Vector2.Distance(transform.position, Target.Collider2D.ClosestPoint(transform.position)) >
            stats.attackRange) return;

        Attack();
    }

    private void Attack()
    {
        _navMeshAgent.speed = 0f;
        _monsterAttack.Attack(Target);
        _navMeshAgent.speed = 1f;
    }

    public void OnInit()
    {
    }

    public void OnReturn()
    {
        CanAttack = false;
        CanMove = false;
        Target = null;
    }
}