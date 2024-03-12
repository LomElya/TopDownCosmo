using System;
using System.Collections.Generic;
using Zenject;

public class InteractableController : IDisposable, ITickable
{
    private InteractableSpawner _spawner;
    private InteractableMover _mover;

    private List<Interactable> _interactables = new List<Interactable>();
    private IPauseHandler _pauseHandler;

    private bool _isLevelRunning = false;
    private bool _isPaused => _pauseHandler.IsPaused;

    private DiContainer _diContainer;

    [Inject]
    private void Construct(InteractableSpawner spawner, InteractableMover mover, IPauseHandler pauseHandler, DiContainer diContainer)
    {
        _spawner = spawner;
        _mover = mover;

        _pauseHandler = pauseHandler;
        _diContainer = diContainer;

    }

    public void Tick()
    {
        if (!_isLevelRunning)
            return;

        if (_isPaused)
            return;

        for (int i = 0; i < _interactables.Count; i++)
            _mover.Move(_interactables[i]);
    }

    public void StartProcess(UserLevelState levelState)
    {
        Cleanup();

        _isLevelRunning = true;

        _spawner.InteractableActive += AddInteractable;
        _spawner.InteractableHidded += RemoveInteractable;

        _mover.StartProcess(levelState);
    }

    public void EndProcess()
    {
        _isLevelRunning = false;

        Cleanup();

        _spawner.InteractableActive -= AddInteractable;
        _spawner.InteractableHidded -= RemoveInteractable;
    }

    private void AddInteractable(Interactable interactable)
    {
        if (!_interactables.Contains(interactable))
        {
            _interactables.Add(interactable);
            _diContainer.Inject(interactable);
            interactable.Interacted += Interacted;
        }
    }

    private void RemoveInteractable(Interactable interactable)
    {
        if (_interactables.Contains(interactable))
        {
            _interactables.Remove(interactable);
            interactable.Interacted -= Interacted;
        }
    }

    private void Interacted(Interactable interactable)
    {
        _spawner.Hide(interactable);
    }

    private void Cleanup()
    {
        while (_interactables.Count != 0)
        {
            for (int i = 0; i < _interactables.Count; i++)
                _spawner.Hide(_interactables[i]);
        }
    }

    public void Dispose()
    {
        _spawner.InteractableActive -= AddInteractable;
        _spawner.InteractableHidded -= RemoveInteractable;
    }


}
