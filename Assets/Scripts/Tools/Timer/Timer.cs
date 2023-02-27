using System;
using System.Collections;
using UnityEngine;

public class Timer
{
    public float passedTime { get; private set; }
    public float progress { get; private set; }

    private bool isTimerStarted;

    public delegate void TimeOver();
    public event TimeOver OnTimeOver;
    public void StartTimer(float seconds)
    {
        if (!isTimerStarted)
        {
            isTimerStarted = true;
            passedTime = 0;
            progress = 0;
            Coroutines.StartRoutine(startCouting(seconds));
        }
        else
        {
            throw new Exception("This timer already start");
        }
    }
    private IEnumerator startCouting(float seconds)
    {
        while (seconds > passedTime)
        {
            passedTime += Time.deltaTime;
            progress = passedTime / seconds;
            yield return null;
        }
        OnTimeOver?.Invoke();
        isTimerStarted = false;
    }
}
