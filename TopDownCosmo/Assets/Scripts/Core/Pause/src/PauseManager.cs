using System;
using System.Collections.Generic;

public class PauseManager : IPauseHandler
{
    public event Action<bool> Paused;

    public bool IsPaused { get; private set; }

    private readonly List<IPauseHandler> _handlers = new();

    public void Register(IPauseHandler handler) => _handlers.Add(handler);
    public void UnRegister(IPauseHandler handler) => _handlers.Remove(handler);

    public void SetPaused(bool isPaused)
    {
        IsPaused = isPaused;

        Paused?.Invoke(IsPaused);

        foreach (var handler in _handlers)
            handler.SetPaused(isPaused);
    }
}
