using System;

public interface IPauseHandler
{
    event Action<bool> Paused;
    bool IsPaused { get; }
    void SetPaused(bool isPaused);
}
