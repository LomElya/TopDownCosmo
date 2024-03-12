using UnityEngine;
using Zenject;

public class ScoreTableMenu : MainMenu
{
    [SerializeField] private ScoreTablePanel _scoreTablePanel;

    private LevelConfig _levelConfig;

    [Inject]
    private void Construct(LevelConfig levelConfig)
    {
        _levelConfig = levelConfig;
    }

    protected override void OnShow()
    {
        _scoreTablePanel.Show(_levelConfig.LevelData);
    }
}
