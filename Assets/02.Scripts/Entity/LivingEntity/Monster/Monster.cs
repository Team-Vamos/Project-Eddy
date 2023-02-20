public class Monster : LivingEntity, IDamageTaker
{
    public MonsterStats monsterStats;
    
    public void Init()
    {
        Health = monsterStats.health;
    }
}