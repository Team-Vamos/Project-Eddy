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

    private List<StatModifier> _statModifiers = new();

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

        _statModifiers.ForEach(x =>
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
        _statModifiers = _statModifiers.OrderByDescending(x => x.Order).ToList();
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
    public UpgradableStat Atk = new(StatType.Atk);
    public UpgradableStat Spd = new(StatType.Spd);
    public UpgradableStat AtkSpd = new(StatType.AtkSpd);
    public UpgradableStat Hp = new(StatType.Hp);
    public UpgradableStat Armor = new(StatType.Armor);
    public UpgradableStat Mine = new(StatType.Mine);
}

public class UpgradableStat
{
    public int Level;
    public float Value;

    private StaticPlayerStat _playerStatData;
    public StatType StatType { get; private set; }

    public UpgradableStat(StatType statType)
    {
        StatType = statType;
    }

    public void SetPlayerStatData(StaticPlayerStat playerStatData)
    {
        _playerStatData = playerStatData;
        Value = StatType switch
        {
            StatType.Atk => _playerStatData.atk,
            StatType.Spd => _playerStatData.spd,
            StatType.AtkSpd => _playerStatData.atkSpd,
            StatType.Hp => _playerStatData.hp,
            StatType.Armor => _playerStatData.armor,
            StatType.Mine => _playerStatData.mine,
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}