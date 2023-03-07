using System;
using UnityEngine;

[Serializable]
public class MonsterData
{
    public MonsterType type;
    public int count;

    public GameObject Prefab => MonsterPrefabListData.Instance.GetPrefab(type);
}