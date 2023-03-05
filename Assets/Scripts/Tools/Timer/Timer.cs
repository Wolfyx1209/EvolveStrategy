using System;
using System.Collections;
using UnityEngine;

public class Timer
{
    public float passedTime { get; private set; }
    public float progress { get; private set; }

    private bool _isTimerStarted;
    private float _seconds;

    public delegate void TimeOver();
    public event TimeOver OnTimeOver;
    public void StartTimer(float seconds)
    {
        if (!_isTimerStarted)
        {
            _isTimerStarted = true;
            passedTime = 0;
            progress = 0;
            _seconds = seconds;
            Coroutines.StartRoutine(startCouting(_seconds));
        }
        else
        {
            throw new Exception("This timer already start");
        }
    }

    public void StopTimer() 
    {
        if (_isTimerStarted) 
        {
            Coroutines.StopRoutine(startCouting(_seconds));
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
        _isTimerStarted = false;
        OnTimeOver?.Invoke();
    }
}
