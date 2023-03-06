﻿using UnityEngine;

public class PlayerStatData : ScriptableObject
{
    [SerializeField] private StaticPlayerStat[] playerStats;
    
    public StaticPlayerStat this[int level] => playerStats[level - 1];
}