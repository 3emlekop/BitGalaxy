using System;
using Unity.VisualScripting;
using UnityEngine;

public class Timer
{
    private Action action;
    private float time;
    private bool isRunning = false;
    private bool isLooped = false;
    private float startTime;
    private byte iterations = 1;
    private bool destroyOnEnd = false;
    private TimerBehaviour timerBehaviour;

    private class TimerBehaviour : MonoBehaviour
    {
        public Action onUpdate;
        private void Update()
        {
            if (onUpdate != null)
                onUpdate();
        }
    }

    #region Timer Constructors
    private Timer(Action action, float time, byte iterations, TimerBehaviour timerBehaviour)
    {
        if (action != null)
            this.action = action;
        if (time >= 0)
        {
            this.time = time;
            startTime = time;
        }
        isRunning = true;
        isLooped = false;
        this.iterations = iterations;
        this.timerBehaviour = timerBehaviour;
    }

    private Timer(Action action, float time, bool isLooped, TimerBehaviour timerBehaviour)
    {
        if (action != null)
            this.action = action;
        if (time >= 0)
        {
            this.time = time;
            startTime = time;
        }
        isRunning = true;
        this.isLooped = isLooped;
        destroyOnEnd = !isLooped;
        iterations = 1;
        this.timerBehaviour = timerBehaviour;
    }
    #endregion

    #region Timer Create Overloads
    public static Timer Create(Transform transform, Action action, float time, byte iterations)
    {
        var timerBehaviour = transform.AddComponent<TimerBehaviour>();
        Timer timer = new Timer(action, time, iterations, timerBehaviour);
        timerBehaviour.onUpdate = timer.Tick;

        return timer;
    }

    public static Timer Create(Transform transform, Action action, float time, bool isLooped)
    {
        var timerBehaviour = transform.AddComponent<TimerBehaviour>();
        Timer timer = new Timer(action, time, isLooped, timerBehaviour);
        timerBehaviour.onUpdate = timer.Tick;

        return timer;
    }
    #endregion

    public void Tick()
    {
        if (!isRunning)
            return;

        time -= Time.deltaTime;
        if (time < 0)
        {
            if (iterations < 1)
            {
                if (destroyOnEnd)
                    DestroySelf();
                else
                    Disable();
                return;
            }

            action();
            time = startTime;

            if (isLooped)
                return;
            else
                iterations--;
        }
    }

    public void Enable()
    {
        if (action != null) { isRunning = true; time = startTime; }
    }

    public void Disable()
    {
        if (action != null) { isRunning = false; }
    }

    private void DestroySelf()
    {
        if (timerBehaviour != null)
            UnityEngine.Object.Destroy(timerBehaviour);
        Disable();
    }

    public void SetIterations(byte count)
    {
        iterations = count;
    }
}
