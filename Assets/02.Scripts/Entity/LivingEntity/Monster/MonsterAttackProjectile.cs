using UnityEngine;

public class MonsterAttackProjectile : MonsterAttack
{
    public GameObject projectilePrefab;

    public override void Attack(Entity target)
    {
        var projectile = Instantiate(projectilePrefab, Monster.transform.position, Quaternion.identity);
        var projectileComponent = projectile.GetComponent<Projectile>();
        projectileComponent.Init(Monster);
        projectileComponent.Fire(target.transform.position);
    }
}