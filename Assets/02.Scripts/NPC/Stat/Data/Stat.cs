using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Stat
{
    public string name;
    public float value;

    public Stat(string name, float value)
    {
        this.name = name;
        this.value = value;
    }
}
