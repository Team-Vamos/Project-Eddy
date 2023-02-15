using UnityEngine;

[CreateAssetMenu(menuName = "Create WaveData", fileName = "WaveData", order = 0)]
public class WaveData : ScriptableObject
{
    public MonsterDataList[] waveMonsters;
}