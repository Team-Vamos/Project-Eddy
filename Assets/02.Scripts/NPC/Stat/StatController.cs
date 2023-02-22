using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StatController
{
    [SerializeField]
    private List<Stat> _defaultStats;

    private Dictionary<string, Stat> _statDict = new Dictionary<string, Stat>();

    public event System.Action<string, CalcType> OnStatChanged;

    public void Init() {
        foreach(Stat stat in _defaultStats)
        {
            _statDict.Add(stat.name, stat);
        }
    }

    public Stat GetStat(string name)
    {
        if(!_statDict.TryGetValue(name, out Stat stat))
        {
            throw new System.Exception("Stat is Null in dictionary. Please checkout the parameter name");
        }
        return stat;
    }

    public void CalcStat(Stat addStat, CalcType calcType)
    {
        if(_statDict.TryGetValue(addStat.name, out Stat stat))
            stat.value = CalcStat(stat, addStat, calcType);
        else
            _statDict.Add(addStat.name, addStat);

        // OnStatChanged?.Invoke();
    }

    private float CalcStat(Stat stat, Stat addStat, CalcType calcType) => calcType switch
    {
        CalcType.Add => stat.value + addStat.value,
        CalcType.Subtract => stat.value - addStat.value,
        CalcType.Multiply => stat.value * addStat.value,
        CalcType.Divide => stat.value / addStat.value,
        _ => throw new System.Exception("CalcType is Wrong")
    };

}
