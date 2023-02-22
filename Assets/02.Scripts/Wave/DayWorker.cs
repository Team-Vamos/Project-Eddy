using System;
using System.Collections;
using UnityEngine;

public class DayWorker : MonoBehaviour
{
    #region Events
    public delegate void OnStatusChangingDelegate(DayState dayState, float duration);
    public delegate void OnStatusChangedDelegate(DayState dayState);

    public event OnStatusChangingDelegate OnStatusChanging;
    public event OnStatusChangedDelegate OnStatusChanged;
    #endregion

    public float dayTime = 60;
    public float nightTime = 60;

    public float changeDuration = 1;

    public float dayTimeScale = 1;
    public float nightTimeScale = 1;

    [SerializeField] private TimeWorker timeWorker;

    [field:SerializeField] public DayState DayState { get; private set; } = DayState.Day;
    
    private bool _isChanging;

    private void Update()
    {
        if (_isChanging) return;

        switch (DayState)
        {
            case DayState.Day when !(timeWorker.time >= dayTime):
                return;
            case DayState.Day:
                DayState = DayState.Night;
                OnStatusChanging?.SafeInvoke(DayState, changeDuration);
                StartCoroutine(ChangeState());
                break;
            
            case DayState.Night when !(timeWorker.time >= nightTime):
                return;
            case DayState.Night:
                DayState = DayState.Day;
                OnStatusChanging?.SafeInvoke(DayState, changeDuration);
                StartCoroutine(ChangeState());
                break;
            
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private IEnumerator ChangeState()
    {
        timeWorker.ResetTime();
        timeWorker.Pause();
        _isChanging = true;
        yield return new WaitForSeconds(changeDuration);
        
        if (DayState == DayState.Day)
        {
            OnStatusChanged?.SafeInvoke(DayState);
            timeWorker.SetTimeScale(dayTimeScale);
        }
        else
        {
            OnStatusChanged?.SafeInvoke(DayState);
            timeWorker.SetTimeScale(nightTimeScale);
        }
        
        timeWorker.Resume();
        _isChanging = false;
    }
}