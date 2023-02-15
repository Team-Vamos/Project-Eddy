public class Monster : Entity, IDamageTaker
{
    public delegate void OnDamageTaken(int damage);
    public delegate void OnDeath();
    
    public OnDamageTaken onDamageTaken;
    public OnDeath onDeath;
    
    public MonsterStats monsterStats;

    public int Health { get; private set; }
    
    public void Init()
    {
        Health = monsterStats.health;
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
        onDamageTaken?.Invoke(damage);
        
        if (Health <= 0)
        {
            onDeath?.Invoke();
        }
    }
}