using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

public sealed class SurviveScenarioProcessor : IDisposable
{
    public event Action<InteractableType> InteractableSpawn;

    private InteractableController _controller;

    private readonly IPauseHandler _pauseHandler;

    private readonly Level _level;
    private readonly Timer _timer;

    private UserLevelState _levelState;

    public bool IsRunning { get; private set; }
    public bool IsPaused => _pauseHandler.IsPaused;

    public SurviveScenarioProcessor(Level level, IPauseHandler pauseHandler, InteractableController interactableController)
    {
        _pauseHandler = pauseHandler;

        _level = level;

       // _timer = _level.Timer;

        _controller = interactableController;
    }

    public void StartProcess(UserLevelState levelState)
    {
        _levelState = levelState;

        IsRunning = true;

       // _timer.TimeIsOver += TimeOver;

        _controller.StartProcess(_levelState);

        var interactables = levelState.InteractableSpawn;

        foreach (var interactableData in interactables)
        {
            SpawnInteractable(interactableData);
        }
    }

    public void EndProcess()
    {
        _controller.EndProcess();

       // _timer.TimeIsOver -= TimeOver;
        IsRunning = false;
    }

    private async UniTask SpawnInteractable(InteractableSpawnState interactableSpawnData)
    {
        var cooldown = interactableSpawnData.StartCooldown;

        await UniTask.Delay(TimeSpan.FromSeconds(interactableSpawnData.PrewarmTime));

        while (IsRunning)
        {
            while (IsPaused)
                await UniTask.Yield();

            InteractableSpawn?.Invoke(interactableSpawnData.InteractableType);

            await UniTask.Delay(TimeSpan.FromSeconds(cooldown));
            cooldown = Mathf.Lerp(interactableSpawnData.StartCooldown,
                interactableSpawnData.EndCooldown, (_timer.RemainigTime / _levelState.LevelLength));
        }
    }

    private void TimeOver()
    {
        EndProcess();
    }

    public void Dispose()
    {
        IsRunning = false;

        //_timer.TimeIsOver -= TimeOver;
    }
}