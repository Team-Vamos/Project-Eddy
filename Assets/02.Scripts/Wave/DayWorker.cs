using System.Collections;
using UnityEngine;

public enum DayState
{
    Day,
    Night,
}

public class DayWorker : MonoBehaviour
{
    public delegate void OnNightComingDelegate();
    public delegate void OnDayComingDelegate();
    public delegate void OnNightComeDelegate();
    public delegate void OnDayComeDelegate();

    public event OnNightComingDelegate OnNightComing;
    public event OnDayComingDelegate OnDayComing;
    public event OnNightComeDelegate OnNightCome;
    public event OnDayComeDelegate OnDayCome;

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
            OnNightComing?.Invoke();
            StartCoroutine(ChangeState());
        }
        else
        {
            if (!(timeWorker.time >= nightTime)) return;
            
            _dayState = DayState.Day;
            OnDayComing?.Invoke();
            StartCoroutine(ChangeState());
        }
    }

    private IEnumerator ChangeState()
    {
        yield return new WaitForSeconds(changeDuration);
        
        if (_dayState == DayState.Day)
        {
            OnDayCome?.Invoke();
            timeWorker.SetTimeScale(dayTimeScale);
        }
        else
        {
            OnNightCome?.Invoke();
            timeWorker.SetTimeScale(nightTimeScale);
        }
    }
}