using System;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class MonsterData
{
    public MonsterType type;
    public int count;

    public GameObject Prefab => new GameObject();
}