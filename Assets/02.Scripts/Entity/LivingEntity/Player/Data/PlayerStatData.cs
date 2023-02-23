using UnityEngine;
using UnityEngine.Serialization;

public class PlayerStatData : ScriptableObject
{
    [SerializeField] private PlayerStat[] playerStats;
    
    public PlayerStat this[int level] => playerStats[level - 1];
}