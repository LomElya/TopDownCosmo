using UnityEngine.Events;
using Zenject;

public class GameProcessorCommand : Command
{
    private SurviveScenarioProcessor _processor;
    private ShipSpawner _shipSpawner;
    private GUIHolder _guiHolder;

    private Timer _timer;

    private Score _score;
    private Wallet _wallet;

    [Inject]
    private void Construct(SurviveScenarioProcessor processor, GUIHolder guiHolder, ShipSpawner shipSpawner, Level level, Score score, Wallet wallet)
    {
        _processor = processor;
        _guiHolder = guiHolder;
        _shipSpawner = shipSpawner;

        _score = score;
        _wallet = wallet;

        _timer = level.Timer;
    }

    public GameProcessorCommand(GameProcessorData data) : base(data)
    {
    }

    public override void Execute(UnityAction onCompleted)
    {
        _timer.StopCountingTime();

        var processorCommand = (GameProcessorData)_commandData;

        var levelState = processorCommand.LevelState;

        float levelLength = levelState.LevelLength;

        _timer.Set(levelLength);
        _timer.StartCountingTime();

        _processor.StartProcess(levelState);
        _guiHolder.StartGame(levelState);
        _shipSpawner.StartGame();

        _score.StartLevel();
        _wallet.StartLevel();

        onCompleted?.Invoke();
    }
}
