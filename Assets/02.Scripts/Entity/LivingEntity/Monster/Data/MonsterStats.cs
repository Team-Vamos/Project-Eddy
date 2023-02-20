using UnityEngine;

[CreateAssetMenu(menuName = "Create MonsterStats", fileName = "MonsterStats", order = 0)]
public class MonsterStats : ScriptableObject
{
    public TargetingType targetingType;
    public string monsterName;
    public AttackType attackType;
    public DamageType damageType;
    public int health;
    public int damage;
    public float attackSpeed;
    public float speed;
    public float attackRange;
    public int dropExp;
    public RarityType dropBoxRarity;
    public float dropBoxChance;
}