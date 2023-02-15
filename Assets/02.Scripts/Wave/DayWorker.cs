using System.Collections;
using UnityEngine;

public class DayWorker : MonoBehaviour
{
    public delegate void OnStatusChangingDelegate(DayState dayState);
    public delegate void OnStatusChangedDelegate(DayState dayState);

    public event OnStatusChangingDelegate OnStatusChanging;
    public event OnStatusChangedDelegate OnStatusChanged;

    public float dayTime = 60;
    public float nightTime = 60;

    public float changeDuration = 1;

    public float dayTimeScale = 1;
    public float nightTimeScale = 1;

    [SerializeField] private TimeWorker timeWorker;

    private DayState _dayState = DayState.Day;

    private void Update()
    {
        if (_dayState == DayState.Day)
        {
            if (!(timeWorker.time >= dayTime)) return;
            
            _dayState = DayState.Night;
            OnStatusChanging?.Invoke(_dayState);
            StartCoroutine(ChangeState());
        }
        else
        {
            if (!(timeWorker.time >= nightTime)) return;
            
            _dayState = DayState.Day;
            OnStatusChanging?.Invoke(_dayState);
            StartCoroutine(ChangeState());
        }
    }

    private IEnumerator ChangeState()
    {
        yield return new WaitForSeconds(changeDuration);
        
        if (_dayState == DayState.Day)
        {
            OnStatusChanged?.Invoke(_dayState);
            timeWorker.SetTimeScale(dayTimeScale);
        }
        else
        {
            OnStatusChanged?.Invoke(_dayState);
            timeWorker.SetTimeScale(nightTimeScale);
        }
        
        timeWorker.ResetTime();
    }
}