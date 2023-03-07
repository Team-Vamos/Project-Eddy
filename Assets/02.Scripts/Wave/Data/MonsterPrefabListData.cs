using System;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Create MonsterPrefabListData", fileName = "MonsterPrefabListData", order = 0)]
public class MonsterPrefabListData : ScriptableObject
{
    public static MonsterPrefabListData Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = Resources.Load<MonsterPrefabListData>("MonsterPrefabListData");
            }

            return _instance;
        }
    }

    private void OnEnable()
    {
        _instance = this;
    }

    public MonsterPrefabData[] monsterPrefabDataList;
    private static MonsterPrefabListData _instance;

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