using UnityEngine;

public class MonsterRangeAttack : MonoBehaviour
{
    [SerializeField] private Projectile projectile;
    private Monster _monster;

    public void Init(Monster monster)
    {
        _monster = monster;
    }
    
    public void Attack(Vector2 targetPosition)
    {
        var projectile = Instantiate(this.projectile, transform.position, Quaternion.identity);
        projectile.Init(_monster);
        projectile.Fire(targetPosition);
    }
}