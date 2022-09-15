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
        get => _endTime >= Time.time;
    }

    public Timer Start(float time)
    {
        _endTime = Time.time + time;
        return this;
    }
}