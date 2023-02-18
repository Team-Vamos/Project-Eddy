using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StatController
{
    [SerializeField]
    private List<Stat> _stats;

    private Dictionary<string, Stat> _statDict = new Dictionary<string, Stat>();
    public void Init() {
        foreach(Stat stat in _stats)
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
}
