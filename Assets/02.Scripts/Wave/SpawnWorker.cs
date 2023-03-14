using System.Collections;
using EventManagers;
using UnityEngine;

public class SpawnWorker : MonoBehaviour
{
    [SerializeField] private DayWorker dayWorker;
    [SerializeField] private WaveWorker waveWorker;

    [SerializeField] private Vector2 spawnRange;
    [SerializeField] private WaveData waveData;

    private void Awake()
    {
        EventManager.StartListening(NetManager.NetworkConnected, () => { });

        if (!NetManager.Instance.isServer)
        {
            enabled = false;
            return;
        }

        waveWorker.OnWaveStart += OnWaveStart;

        Debug.Log("SpawnWorker Awake");
    }

    private void OnWaveStart(int waveCount, bool isBloodMoon)
    {
        var waveTime = dayWorker.nightTime / 2;
        if (isBloodMoon) waveTime = dayWorker.nightTime;
        if (waveData is null || waveCount - 1 >= waveData.waveMonsters.Length)
        {
            Debug.LogWarning("Wave count is out of range");
            return;
        }

        foreach (var waveMonster in waveData.waveMonsters[waveCount - 1].monsterDataList)
            StartCoroutine(SpawnMonster(waveMonster, waveTime));
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
        var monsterObject = NetworkPoolManager.Instantiate(monsterData.Prefab, spawnPoint, Quaternion.identity);
        var monster = monsterObject.GetComponent<Monster>();
        monster.Init();
    }

    private Vector3 GetSpawnPoint()
    {
        var spawnPoint = new Vector3(0, 0, 0);
        if (Random.Range(0, 2) == 0)
        {
            spawnPoint.x = Random.Range(-spawnRange.x / 2, spawnRange.x / 2);
            spawnPoint.y = Random.Range(0, 2) == 0 ? spawnRange.y / 2 : -spawnRange.y / 2;
        }
        else
        {
            spawnPoint.x = Random.Range(0, 2) == 0 ? spawnRange.x / 2 : -spawnRange.x / 2;
            spawnPoint.y = Random.Range(-spawnRange.y / 2, spawnRange.y / 2);
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