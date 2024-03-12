using Cysharp.Threading.Tasks;
using UnityEngine.Events;
using Zenject;

public class QuitGameCommand : Command
{
    private LoadingScreenProvider _loadingScreenProvider;
    private Gameplay _gameplay;
    private Timer _timer;

    private SurviveScenarioProcessor _processor;

    public QuitGameCommand(QuitGameData data) : base(data)
    {
    }

    [Inject]
    private void Construct(LoadingScreenProvider loadingScreenProvider, Gameplay gameplay, SurviveScenarioProcessor processor, Level level)
    {
        _loadingScreenProvider = loadingScreenProvider;
        _gameplay = gameplay;
        _processor = processor;

        _timer = level.Timer;
    }

    public override void Execute(UnityAction onCompleted)
    {
        _timer.StopCountingTime();
        _loadingScreenProvider.LoadAndDestroy(new ClearGameOperation(_gameplay)).Forget();
        _processor.EndProcess();

        onCompleted?.Invoke();
    }
}
