using UnityEngine;

[CreateAssetMenu(menuName = "Create PlayerStatData", fileName = "PlayerStatData", order = 0)]
public class PlayerStatData : ScriptableObject
{
    [SerializeField] private StaticPlayerStat[] playerStats;
    
    public StaticPlayerStat this[int level] => playerStats[level - 1];
}