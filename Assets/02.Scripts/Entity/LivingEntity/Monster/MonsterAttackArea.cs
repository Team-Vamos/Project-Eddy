using UnityEngine;

public class MonsterAttackArea : MonsterAttack
{
    public override void Attack(Entity _)
    {
        foreach (var entity in EntityManager.Instance.Entities)
        {
            if (entity == Monster) continue;
            if (entity is not IDamageTaker damageTaker) continue;
            if (entity.EntityType.HasFlag(EntityType.Monster)) continue;
            if (Vector2.Distance(Monster.transform.position,
                    entity.Collider2D.ClosestPoint(Monster.transform.position)) >
                Monster.stats.attackRange) continue;
            damageTaker.TakeDamage(Monster.stats.damage);
        }
    }
}