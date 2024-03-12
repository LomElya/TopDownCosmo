using System;
using UnityEngine;

public class Level
{
    public event Action<int> LevelChange;

    public Timer Timer { get; private set; }

    private IStateCommunicator _stateCommunicator;
    private UserContainer _userContainer;
    private UserState _userState => _userContainer.State;

    public Level(Timer timer, IStateCommunicator stateCommunicator, UserContainer userContainer)
    {
        Timer = timer;

        _stateCommunicator = stateCommunicator;
        _userContainer = userContainer;
    }

    public void ChangeLevel(int id)
    {
        if (GetCurrentLevelID() == id)
            return;

        LevelChange?.Invoke(id);

        _userContainer.ChangeLevel(id);
        _stateCommunicator.SaveState(_userState);
    }

    public void NextLevel()
    {
        if (IsLastLevel())
            return;

        int nextLevel = GetCurrentLevelID() + 1;

        if (!_userContainer.IsOpenedLevel(nextLevel))
            _userContainer.OpenLevel(nextLevel);

        ChangeLevel(nextLevel);
    }

    public void OpenLevel()
    {
        if (IsLastLevel())
            return;

        int nextLevel = GetCurrentLevelID() + 1;

        if (!_userContainer.IsOpenedLevel(nextLevel))
            _userContainer.OpenLevel(nextLevel);
    }

    public int GetCurrentLevelID() => _userContainer.CurrentLevel();
    public bool IsLastLevel() => _userContainer.IsLastLevel(GetCurrentLevelID());
}
