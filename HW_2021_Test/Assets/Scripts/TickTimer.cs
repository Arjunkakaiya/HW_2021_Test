using System;
using UnityEngine;

public class TickTimer : MonoBehaviour
{
    public class OnTickEventArgs : EventArgs
    {
        public float _ticks;
    }

    public static event EventHandler OnTick;
    public static event EventHandler<OnTickEventArgs> OnUpdateTick;
    private const float MAX_TIMER_TICK = 1f;
    private Canvas canvas;
    private int ticks;
    private float tickTimer;

    private void Awake()
    {
        canvas = GetComponentInParent<Canvas>();
    }

    private void OnEnable()
    {
        ticks = 0;
    }

    private void Update()
    {
        // if (!canvas.enabled) return;
        tickTimer += Time.deltaTime;

        if (OnUpdateTick != null) OnUpdateTick?.Invoke(this, new OnTickEventArgs { _ticks = Time.deltaTime });

        if (tickTimer >= MAX_TIMER_TICK)
        {
            tickTimer -= MAX_TIMER_TICK;
            ticks++;
            if (OnTick != null) OnTick?.Invoke(this, new EventArgs());
        }
    }
}
