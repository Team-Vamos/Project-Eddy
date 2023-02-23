using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerStatStorage : MonoBehaviour
{
    [SerializeField] private PlayerStatData playerStatData;

    [SerializeField] private int level;
    [SerializeField] private int exp;
    [SerializeField] private int expToNextLevel;

    [SerializeField] private PlayerStat stat;

    public List<StatModifier> statModifiers = new List<StatModifier>();

    public int Level => level;
    public int Exp => exp;
    public int ExpToNextLevel => expToNextLevel;
    public PlayerStat Stat => stat;

    public void AddExp(int point)
    {
        exp += point;

        if (exp < expToNextLevel) return;

        exp -= expToNextLevel;
        level++;

        CalculateStats();
    }

    private void CalculateStats()
    {
        stat = playerStatData[level];
        
        statModifiers.OrderByDescending(x => x.Order).ToList().ForEach(x =>
        {
            switch (x.Type)
            {
                case StatModifierType.Additive:
                    stat.Add(x.StatType, x.Value);
                    break;
                case StatModifierType.Multiplicative:
                    stat.Multiply(x.StatType, x.Value);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        });
        
        expToNextLevel = playerStatData[level + 1].expToNextLevel;
    }
    
    public void AddStatModifier(StatModifier statModifier)
    {
        statModifiers.Add(statModifier);
        CalculateStats();
    }
    
    public void RemoveStatModifier(StatModifier statModifier)
    {
        statModifiers.Remove(statModifier);
        CalculateStats();
    }
    
    public void RemoveAllStatModifiersFromSource(object source)
    {
        statModifiers.RemoveAll(x => x.Source == source);
        CalculateStats();
    }
}

[Serializable]
public class StatModifier
{
    public object Source { get; }
    public float Value { get; }
    public StatModifierType Type { get; }
    public StatType StatType { get; }
    public int Order { get; }

    public StatModifier(object source, float value, StatModifierType type, StatType statType, int order = 0)
    {
        Source = source;
        Value = value;
        Type = type;
        StatType = statType;
        Order = order;
    }
}

public enum StatModifierType
{
    Additive,
    Multiplicative
}