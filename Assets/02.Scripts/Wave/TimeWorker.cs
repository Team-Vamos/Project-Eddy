using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeWorker : MonoBehaviour
{
    public delegate void OnTimeResetDelegate();
    
    public event OnTimeResetDelegate OnTimeReset;

    public float time;
    public float timeScale = 1;
    public bool isPause;

    public void Pause()
    {
        isPause = true;
    }

    public void Resume()
    {
        isPause = false;
    }

    public void SetTimeScale(float scale)
    {
        timeScale = scale;
    }

    public void SetTime(float time)
    {
        this.time = time;
    }

    public void AddTime(float time)
    {
        this.time += time;
    }
    
    public void ResetTime()
    {
        time = 0;
        OnTimeReset?.Invoke();
    }

    public void Update()
    {
        if (!isPause)
        {
            time += Time.deltaTime * timeScale;
        }
    }
}