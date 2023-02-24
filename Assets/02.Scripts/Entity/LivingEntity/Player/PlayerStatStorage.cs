using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerStatStorage : MonoBehaviour
{
    private const int StatPointPerLevel = 3;

    [SerializeField] private PlayerStatData playerStatData;

    [SerializeField] private int level = 1;
    [SerializeField] private int exp;
    [SerializeField] private int expToNextLevel;

    [SerializeField] private int statPoint;

    private readonly PlayerStat _stat = new();

    private readonly List<StatModifier> _statModifiers = new();

    public int Level => level;
    public int Exp => exp;
    public int ExpToNextLevel => expToNextLevel;
    public PlayerStat Stat => _stat;

    public void AddExp(int point)
    {
        exp += point;

        if (exp < expToNextLevel) return;

        exp -= expToNextLevel;

        level++;
        statPoint += StatPointPerLevel;

        if (playerStatData[level] is null)
        {
            level--;
            statPoint -= StatPointPerLevel;
            exp = expToNextLevel;
            throw new Exception("Not enough level data");
        }

        expToNextLevel = playerStatData[level].expToNextLevel;

        CalculateStats();
    }

    private void CalculateStats()
    {
        _stat.Atk.SetPlayerStatData(playerStatData[level]);
        _stat.Spd.SetPlayerStatData(playerStatData[level]);
        _stat.AtkSpd.SetPlayerStatData(playerStatData[level]);
        _stat.Hp.SetPlayerStatData(playerStatData[level]);
        _stat.Armor.SetPlayerStatData(playerStatData[level]);
        _stat.Mine.SetPlayerStatData(playerStatData[level]);

        _statModifiers.OrderByDescending(x => x.Order).ToList().ForEach(x =>
        {
            switch (x.StatType)
            {
                case StatType.Atk:
                    x.CalculateStat(ref _stat.Atk);
                    break;
                case StatType.Spd:
                    x.CalculateStat(ref _stat.Spd);
                    break;
                case StatType.AtkSpd:
                    x.CalculateStat(ref _stat.AtkSpd);
                    break;
                case StatType.Hp:
                    x.CalculateStat(ref _stat.Hp);
                    break;
                case StatType.Armor:
                    x.CalculateStat(ref _stat.Armor);
                    break;
                case StatType.Mine:
                    x.CalculateStat(ref _stat.Mine);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        });
    }

    public void AddStatModifier(StatModifier statModifier)
    {
        _statModifiers.Add(statModifier);
        CalculateStats();
    }

    public void RemoveStatModifier(StatModifier statModifier)
    {
        _statModifiers.Remove(statModifier);
        CalculateStats();
    }

    public void RemoveAllStatModifiersFromSource(object source)
    {
        _statModifiers.RemoveAll(x => x.Source == source);
        CalculateStats();
    }
    
    public void UpgradeStat(StatType statType)
    {
        if (statPoint <= 0) 
            throw new Exception("Not enough stat point");

        statPoint--;

        switch (statType)
        {
            case StatType.Atk:
                _stat.Atk.Level++;
                break;
            case StatType.Spd:
                _stat.Spd.Level++;
                break;
            case StatType.AtkSpd:
                _stat.AtkSpd.Level++;
                break;
            case StatType.Hp:
                _stat.Hp.Level++;
                break;
            case StatType.Armor:
                _stat.Armor.Level++;
                break;
            case StatType.Mine:
                _stat.Mine.Level++;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(statType), statType, null);
        }

        CalculateStats();
    }
}

public class PlayerStat
{
    public UpgradableStat Atk = new();
    public UpgradableStat Spd = new();
    public UpgradableStat AtkSpd = new();
    public UpgradableStat Hp = new();
    public UpgradableStat Armor = new();
    public UpgradableStat Mine = new();
}

public class UpgradableStat
{
    public int Level;
    public float Value;
    
    private StaticPlayerStat _playerStatData;
    
    public void SetPlayerStatData(StaticPlayerStat playerStatData)
    {
        _playerStatData = playerStatData;
        Value = playerStatData.atk;
    }
}