using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class GUIHolder : MonoBehaviour
{
    public event Action QuitGame;

    [SerializeField] private TileMover _tileMover;

    [SerializeField] private Button _exitButton;
    private IPauseHandler _pauseHandler;

    private UserLevelState _userLevelState;

    private SignalBus _signalBus;

    [Inject]
    private void Construct(IPauseHandler pauseHandler, SignalBus signalBus)
    {
        _pauseHandler = pauseHandler;
        _signalBus = signalBus;

        _pauseHandler.Paused += OnPause;

        _exitButton.onClick.AddListener(OnExitButtonClick);
    }

    public void StartGame(UserLevelState userLevelState)
    {
        _userLevelState = userLevelState;

        _tileMover.Init(_userLevelState);
        _tileMover.StartMove();
    }

    private async void OnExitButtonClick()
    {
        OnPauseButtonClick(true);

        var alertPopup = await PopupAlert.Load();
        var isConfirmed = await alertPopup.Value.AwaitForDecision("Выйти в главное меню?");

        OnPauseButtonClick(false);

        if (isConfirmed)
            QuitGame?.Invoke();

        alertPopup.Dispose();
    }

    private void OnPauseButtonClick(bool isPaused)
    {
        _signalBus.Fire(new PauseGameSignal(isPaused));
    }

    private void OnPause(bool isPaused)
    {
        _tileMover.SetPaused(isPaused);
    }

    private void OnDisable()
    {
        _exitButton.onClick.RemoveListener(OnExitButtonClick);
        _pauseHandler.Paused -= OnPause;
    }
}
