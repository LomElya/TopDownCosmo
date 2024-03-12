using System;

public class Score
{
    public event Action<int> ScoreChange;

    private IStateCommunicator _stateCommunicator;
    private UserContainer _userContainer;
    private UserState _userState => _userContainer.State;

    private int _currentScoreLevel = 0;

    public Score(IStateCommunicator stateCommunicator, UserContainer userContainer)
    {
        _stateCommunicator = stateCommunicator;
        _userContainer = userContainer;
    }

    public void StartLevel()
    {
        _currentScoreLevel = 0;
        ScoreChange?.Invoke(_currentScoreLevel);
    }

    public void ChangeScore(int value)
    {
        _currentScoreLevel += value;

        ScoreChange?.Invoke(_currentScoreLevel);
    }

    public void SaveTotalScore()
    {
        _userContainer.ChangeScore(_currentScoreLevel);
        _stateCommunicator.SaveState(_userState);
    }

    public int GetCurrentScore() => _currentScoreLevel;
    public int GetTotalLeveltScore() => _userContainer.GetCurrentLevelScore();
}
