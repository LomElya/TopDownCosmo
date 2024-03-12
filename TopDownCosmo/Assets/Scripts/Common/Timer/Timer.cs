using System;
using System.Collections;
using UnityEngine;

public class Timer : IDisposable
{
    private IEnumerator _countdown;

    private MonoBehaviour _context;

    public event Action HasBeenUpdated;
    public event Action TimeIsOver;

    public Timer(MonoBehaviour context) => _context = context;

    public float MaxTime { get; private set; }
    public float RemainigTime { get; private set; }

    private bool _isPaused;

    public void Set(float maxTime, float currentTime = 0f)
    {
        if (maxTime < currentTime)
            throw new ArgumentOutOfRangeException(nameof(currentTime));

        MaxTime = maxTime;
        RemainigTime = currentTime;

        InvokeUpdate();
    }

    public void StartCountingTime()
    {
        Continue();
        _countdown = Countdown();

        _context.StartCoroutine(_countdown);
    }

    public void StopCountingTime()
    {
        if (_countdown != null)
            _context.StopCoroutine(_countdown);

        RemainigTime = MaxTime;
        Stop();
    }

    public void Continue() => _isPaused = false;
    public void Stop() => _isPaused = true;
    public void SetPaused(bool isPaused) => _isPaused = isPaused;

    private IEnumerator Countdown()
    {
        while (RemainigTime <= MaxTime)
        {
            while (_isPaused == true)
                yield return null;

            RemainigTime += Time.deltaTime;

            InvokeUpdate();

            yield return null;
        }

        TimeIsOver?.Invoke();
    }

    private void InvokeUpdate() => HasBeenUpdated?.Invoke();

    public void Dispose()
    {
        if (_countdown != null)
            _context.StopCoroutine(_countdown);
    }
}
