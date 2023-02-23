using System.Collections;
using System;
using UnityEngine;

public class Timer 
{
    public float passedTime { get; private set; }
    public float process { get; private set; }

    private bool isTimerStarted;
    public void StartTimer(float seconds) 
    {
        if (!isTimerStarted) 
        { 
            isTimerStarted = true;
            passedTime = 0;
            process = 0;
            startCouting(seconds);
        }
        else 
        {
            throw new Exception("This timer already start");
        }
    }
    private IEnumerator startCouting(float seconds) 
    {
        while(seconds> passedTime) 
        {
            passedTime += Time.deltaTime;
            process = passedTime/seconds;
            yield return null;
        }
        isTimerStarted=false;
    } 
}
