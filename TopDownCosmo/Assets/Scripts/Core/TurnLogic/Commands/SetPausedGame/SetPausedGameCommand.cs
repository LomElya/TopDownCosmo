using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class SetPausedGameCommand : Command
{
    private IPauseHandler _pauseHandler;

    private Timer _timer;

    public SetPausedGameCommand(SetPausedGameData data) : base(data)
    {
    }

    [Inject]
    private void Construct(IPauseHandler pauseHandler, Level level)
    {
        _pauseHandler = pauseHandler;
        _timer = level.Timer;
    }

    public override void Execute(UnityAction onCompleted)
    {
        var pausedGameData = (SetPausedGameData)_commandData;

        bool isPaused = pausedGameData.IsPaused;

        _pauseHandler.SetPaused(isPaused);
        _timer.SetPaused(isPaused);

        onCompleted?.Invoke();
    }
}
