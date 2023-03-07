using System;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Create MonsterPrefabListData", fileName = "MonsterPrefabListData", order = 0)]
public class MonsterPrefabListData : ScriptableObject
{
    public static MonsterPrefabListData Instance { get; private set; }

    private void OnEnable()
    {
        Instance = this;
    }

    public MonsterPrefabData[] monsterPrefabDataList;

    public GameObject GetPrefab(MonsterType type)
    {
        return (from monsterPrefabData in monsterPrefabDataList
            where monsterPrefabData.type == type
            select monsterPrefabData.prefab).FirstOrDefault();
    }
}

[Serializable]
public class MonsterPrefabData
{
    public MonsterType type;
    public GameObject prefab;
}