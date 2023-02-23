using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NPCStatController
{
    [SerializeField]
    private List<NPCStat> _defaultStats = new List<NPCStat> { new NPCStat("Spd" , 7) , new NPCStat("RunSpd" , 7) };

    private Dictionary<string, NPCStat> _statDict = new Dictionary<string, NPCStat>();

    public event System.Action<string, CalcType> OnStatChanged;

    public void Init() {
        foreach(NPCStat stat in _defaultStats)
        {
            _statDict.Add(stat.name, stat);
        }
    }

    public NPCStat GetStat(string name)
    {
        if(!_statDict.TryGetValue(name, out NPCStat stat))
        {
            throw new System.Exception("Stat is Null in dictionary. Please checkout the parameter name");
        }
        return stat;
    }

    // public void CalcStat(string statName, float value, CalcType calcType)
    // {
    //     if(_statDict.TryGetValue(statName, out Stat stat))
    //         stat.value = CalcStat(stat, value, calcType);
    //     else
    //         _statDict.Add(statName, new Stat(statName, value));
        
    //     
    //     // OnStatChanged?.Invoke();
    // }

    // private float CalcStat(Stat stat, float value, CalcType calcType) => calcType switch
    // {
    //     CalcType.Add => stat.value + value,
    //     CalcType.Subtract => stat.value - value,
    //     CalcType.Multiply => stat.value * value,
    //     CalcType.Divide => stat.value / value,
    //     _ => throw new System.Exception("CalcType is Wrong")
    // };

}
