public class MonsterAttackSingle : MonsterAttack
{
    public override void Attack(Entity target)
    {
        if (target is not IDamageTaker damageTaker) return;
        damageTaker.TakeDamage(Monster.stats.damage);
    }
}