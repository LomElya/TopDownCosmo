using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class AddScoreCommand : Command
{
    private Score _score;

    public AddScoreCommand(AddScoreData data) : base(data)
    {
    }

    [Inject]
    private void Construct(Score score)
    {
        _score = score;
    }

    public override void Execute(UnityAction onCompleted)
    {
        var data = (AddScoreData)_commandData;
        int value = data.Value;

        Debug.Log($"До: текущее: {_score.GetCurrentScore()}  всего: {_score.GetTotalLeveltScore()}");

        _score.ChangeScore(value);
        _score.SaveTotalScore();

        Debug.Log($"После: текущее: {_score.GetCurrentScore()}  всего: {_score.GetTotalLeveltScore()}");

        onCompleted?.Invoke();
    }
}
