using UnityEngine;

public class ScoreTableSlot : MenuSlot
{
    [SerializeField] private StringValueView _levelText;
    [SerializeField] private StringValueView _levelScoreText;

    public int Score { get; private set; }

    public void Init(int id, int score)
    {
        int finalLevel = id + 1;
        Score = score;

        _levelText.Show(finalLevel.ToString());
        _levelScoreText.Show(score.ToString());
    }
}
