using UnityEngine;
using System;

public struct Timer
{
    float _endTime;

    public Timer(float endTime = 0f)
    {
        _endTime = endTime;
    }

    public bool eslaped
    {
        get => _endTime > Time.time;
    }

    static public implicit operator bool(Timer timer)
    {
        return timer.eslaped;
    }

    public void Start(float time)
    {
        _endTime = Time.time + time;
    }
}