using System;
using UnityEngine;

public class WaveWorker : MonoBehaviour
{
    #region Events
    public delegate void OnWaveStartDelegate(int waveCount, bool isBloodMoon);
    public delegate void OnWaveEndDelegate();
    
    public OnWaveStartDelegate OnWaveStart;
    public OnWaveEndDelegate OnWaveEnd;
    #endregion

    [SerializeField] private DayWorker dayWorker;
    [SerializeField] private LightController globalLight;
    
    [SerializeField] private int bloodMoonWave = 5;
    
    [Header("Light Settings")]
    [SerializeField] private float dayIntensity = 1;
    [SerializeField] private float nightIntensity = 0.3f;
    [SerializeField] private Color bloodMoonColor = Color.red;
    [SerializeField] private float bloodMoonIntensity = 0.1f;
    [SerializeField] private OreSpawner oreSpawner;

    public bool WaveProcessing { get; private set; }
    
    private int _waveCount;

    private void Awake()
    {
        dayWorker.OnStatusChanging += OnDayStatusChanging;
        dayWorker.OnStatusChanged += OnDayStatusChanged;
        
        WaveProcessing = dayWorker.DayState == DayState.Night;

        
    }

    private void Start() {
        globalLight.SetIntensity(WaveProcessing ? nightIntensity : dayIntensity);
        oreSpawner.RemoveOres();
        oreSpawner.SpawnOre();
    }

    private void OnDayStatusChanging(DayState daystate, float duration)
    {
        switch (daystate)
        {
            case DayState.Night:
                _waveCount++;
                WaveProcessing = true;
                
                if (_waveCount % bloodMoonWave == 0)
                {
                    globalLight.SetIntensity(bloodMoonIntensity, duration);
                    globalLight.SetColor(bloodMoonColor, duration);
                    oreSpawner.RemoveOres();
                    oreSpawner.SpawnOre();
                }
                else
                {
                    globalLight.SetIntensity(nightIntensity, duration);
                }
                break;
            case DayState.Day:
                WaveProcessing = false;
                globalLight.SetIntensity(dayIntensity, duration);
                globalLight.SetColor(Color.white, duration);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(daystate), daystate, null);
        }
    }

    private void OnDayStatusChanged(DayState dayState)
    {
        switch (dayState)
        {
            case DayState.Night:
                WaveProcessing = true;
                OnWaveStart?.SafeInvoke(_waveCount, _waveCount % bloodMoonWave == 0);
                break;
            case DayState.Day:
                WaveProcessing = false;
                OnWaveEnd?.SafeInvoke();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(dayState), dayState, null);
        }
    }
}