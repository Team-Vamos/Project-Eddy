using System;
using System.Collections;
using UnityEngine;

public class SpawnWorker : MonoBehaviour
{
    [SerializeField] private DayWorker dayWorker;
    [SerializeField] private WaveWorker waveWorker;

    [SerializeField] private Vector2 spawnRange;
    [SerializeField] private WaveData waveData;

    private void Awake()
    {
        waveWorker.OnWaveStart += OnWaveStart;
    }

    private void OnWaveStart(int waveCount, bool isBloodMoon)
    {
        var waveTime = dayWorker.nightTime / 2;
        
        if (isBloodMoon)
        {
            waveTime = dayWorker.nightTime;
        }

        if (waveData is null || waveCount - 1 >= waveData.waveMonsters.Length)
        {
            Debug.LogWarning("Wave count is out of range");
            return;
        }

        foreach (var waveMonster in waveData.waveMonsters[waveCount - 1].monsterDataList)
        {
            StartCoroutine(SpawnMonster(waveMonster, waveTime));
        }
    }

    private IEnumerator SpawnMonster(MonsterData monsterData, float waveTime)
    {
        for (var i = 0; i < monsterData.count; i++)
        {
            SpawnMonster(monsterData);
            yield return new WaitForSeconds(waveTime / monsterData.count);
        }
    }

    private void SpawnMonster(MonsterData monsterData)
    {
        var spawnPoint = GetSpawnPoint();
        var monsterObject = PoolManager.Instantiate(monsterData.Prefab, spawnPoint, Quaternion.identity);
        var monster = monsterObject.GetComponent<Monster>();
        monster.Init();
    }

    private Vector3 GetSpawnPoint()
    {
        var spawnPoint = new Vector3(0, 0, 0);
        if (UnityEngine.Random.Range(0, 2) == 0)
        {
            spawnPoint.x = UnityEngine.Random.Range(-spawnRange.x / 2, spawnRange.x / 2);
            spawnPoint.y = UnityEngine.Random.Range(0, 2) == 0 ? spawnRange.y / 2 : -spawnRange.y / 2;
        }
        else
        {
            spawnPoint.x = UnityEngine.Random.Range(0, 2) == 0 ? spawnRange.x / 2 : -spawnRange.x / 2;
            spawnPoint.y = UnityEngine.Random.Range(-spawnRange.y / 2, spawnRange.y / 2);
        }
        return spawnPoint;
    }

    private void OnDrawGizmos()
    {
        var center = transform.position;
        var size = new Vector3(spawnRange.x, spawnRange.y, 0);
        Gizmos.color = waveWorker.WaveProcessing ? Color.green : Color.red;
        Gizmos.DrawWireCube(center, size);
    }
}