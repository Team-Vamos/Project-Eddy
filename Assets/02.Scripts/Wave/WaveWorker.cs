using System;
using UnityEngine;

public class WaveWorker : MonoBehaviour
{
    [SerializeField] private DayWorker dayWorker;
    [SerializeField] private Vector2 spawnRange;
    
    public bool WaveProcessing { get; private set; }

    private void OnDrawGizmos()
    {
        var center = transform.position;
        var size = new Vector3(spawnRange.x, spawnRange.y, 0);
        Gizmos.color = WaveProcessing ? Color.green : Color.red;
        Gizmos.DrawWireCube(center, size);
    }
    
    
}