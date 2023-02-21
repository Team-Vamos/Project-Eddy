using System;
using System.Collections;
using UnityEngine;

public class SpawnWorker : MonoBehaviour
{
    [SerializeField] DayWorker dayWorker;
    [SerializeField] WaveWorker waveWorker;

    [SerializeField] private Vector2 spawnRange;
    [SerializeField] WaveData waveData;

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

        if (waveData is null || waveCount >= waveData.waveMonsters.Length)
        {
            Debug.LogWarning("Wave count is out of range");
            return;
        }

        foreach (var waveMonster in waveData.waveMonsters[waveCount].monsterDataList)
        {
            StartCoroutine(SpawnMonster(waveMonster, waveTime));
        }
    }

    private IEnumerator SpawnMonster(MonsterData monsterData, float waveTime)
    {
        for (int i = 0; i < monsterData.count; i++)
        {
            SpawnMonster(monsterData);
            yield return new WaitForSeconds(waveTime / monsterData.count);
        }
    }

    private void SpawnMonster(MonsterData monsterData)
    {
        var spawnPoint = GetSpawnPoint();
        var monsterObject = Instantiate(monsterData.Prefab, spawnPoint, Quaternion.identity);
        var monster = monsterObject.GetComponent<Monster>(); // TODO: Set monster stats
        monster.Init();
    }

    private Vector3 GetSpawnPoint()
    {
        var spawnPoint = new Vector3(
            UnityEngine.Random.Range(-spawnRange.x / 2, spawnRange.x / 2),
            UnityEngine.Random.Range(-spawnRange.y / 2, spawnRange.y / 2),
            0
        );
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