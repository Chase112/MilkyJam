using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class MinimalTimer
{
    public MinimalTimer()
    {
        actualTime = 0;
    }

    // actual time of timer; when the timer was last restarted
    [System.NonSerialized]
    public float actualTime = 0;

    // resets actual time
    public void Restart()
    {
        actualTime = Time.time;
    }
    public void Restart(float diff)
    {
        actualTime = Time.time + diff;
    }
    public float ElapsedTime()
    {
        return Time.time - actualTime;
    }

    // returns true if time elapsed from last reset is greather than passed argument (cd)
    public bool IsReady(float cd)
    {
        return Time.time - actualTime >= cd;
    }
    // the same as above but automatically resets if timer was ready
    public bool IsReadyRestart(float cd)
    {
        if (Time.time - actualTime >= cd)
        {
            Restart();
            return true;
        }
        return false;
    }
}

class UpdatedTimer
{
    public UpdatedTimer()
    {
        actualTime = 0;
    }

    // actual time of timer; when the timer was last restarted
    [System.NonSerialized]
    public float actualTime = 0;

    public void Update(float dt)
    {
        actualTime += dt;
    }

    // resets actual time
    public void Restart()
    {
        actualTime = 0;
    }
    public float ElapsedTime()
    {
        return actualTime;
    }

    // returns true if time elapsed from last reset is greather than passed argument (cd)
    public bool IsReady(float cd)
    {
        return actualTime >= cd;
    }
    // the same as above but automatically resets if timer was ready
    public bool IsReadyRestart(float cd)
    {
        if (actualTime >= cd)
        {
            Restart();
            return true;
        }
        return false;
    }
}

/*
 * usage:
 * create timer, set up cd;
 * 
 * if(timer.isReadyRestart())
 *      do something every [cd] time (in seconds)
 */

[System.Serializable]
public class Timer : MinimalTimer
{

    public Timer(float _cd)
    {
        actualTime = 0;
        cd = _cd;
    }
    public Timer()
    {
        actualTime = 0;
    }
    // how much time have to be elapsed from last reset to be ready
    public float cd = 1;

    public float GetCompletionPercent()
    {
        if (cd != 0)
            return Mathf.Clamp01(ElapsedTime() / cd);
        else
            return 1.0f;
    }

    public void RestartRandom(float cdMin, float cdMax)
    {
        actualTime = Time.time;
        cd = UnityEngine.Random.Range(cdMin, cdMax);
    }
    public void RestartRandom(RangedFloat cdRange)
    {
        actualTime = Time.time;
        cd = UnityEngine.Random.Range(cdRange.min, cdRange.max);
    }


    // returns true if time elapsed from last reset is greather than public member of this class (cd)
    public bool IsReady()
    {
        return IsReady(cd);
    }
    public new bool IsReady(float cd)
    {
        return base.IsReady(cd);
    }
    // the same as above but automatically resets if timer was ready
    public bool IsReadyRestart()
    {
        return IsReadyRestart(cd);
    }
    public new bool IsReadyRestart(float cd)
    {
        return base.IsReadyRestart(cd);
    }
}
