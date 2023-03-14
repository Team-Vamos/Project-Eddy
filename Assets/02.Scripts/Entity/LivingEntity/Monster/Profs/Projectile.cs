using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float damage;
    [SerializeField] private float lifeTime;
    [SerializeField] private float time;
    [SerializeField] private Vector2 targetPosition;

    private void Awake()
    {
        time = 0f;
    }

    public void Init(Monster monster)
    {
        damage = monster.stats.damage;
    }

    public void Fire(Vector2 target)
    {
        targetPosition = target;
        transform.LookAt2D(targetPosition);
    }

    protected virtual void Update()
    {
        time += Time.deltaTime;
        if (time > lifeTime)
        {
            Destroy(gameObject);
            return;
        }

        var transformCache = transform;
        transformCache.position += transformCache.right * (speed * Time.deltaTime);
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.TryGetComponent(out IDamageTaker damageTaker)) return;

        var entity = damageTaker as Entity;
        if (entity == null) return;
        if (entity.EntityType.HasFlag(EntityType.Monster)) return;
        damageTaker.TakeDamage(damage);
        NetworkPoolManager.Destroy(gameObject);
    }
}