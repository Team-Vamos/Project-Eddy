using UnityEngine;

public class Monster : LivingEntity, IDamageTaker
{
    public MonsterStats stats;
    
    public void Init()
    {
        if (stats is null)
        {
            Debug.LogWarning("Monster stats is null");
            return;
        }
        
        Health = stats.health;
    }
}