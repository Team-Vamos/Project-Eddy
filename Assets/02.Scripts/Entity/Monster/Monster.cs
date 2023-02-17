public class Monster : Entity, IDamageTaker
{
    public delegate void OnDamageTaken(float damage);
    public delegate void OnDeath();
    
    public OnDamageTaken onDamageTaken;
    public OnDeath onDeath;
    
    public MonsterStats monsterStats;

    public float Health { get; private set; }
    
    public void Init()
    {
        Health = monsterStats.health;
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;
        onDamageTaken?.Invoke(damage);
        
        if (Health <= 0)
        {
            onDeath?.Invoke();
        }
    }
}