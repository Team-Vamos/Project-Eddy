using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct NPCStat
{
    public string name;
    public float value;

    public NPCStat(string name, float value)
    {
        this.name = name;
        this.value = value;
    }
}