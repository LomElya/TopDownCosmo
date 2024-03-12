using System.Collections.Generic;
using Zenject;

public class GameStateStatusCheck : GameState
{
    private SignalBus _signalBus;
    private TurnManager _turnManager;
    private Timer _timer;

    private GUIHolder _gUIHolder;

    private IPauseHandler _pauseHandler;

    public GameStateStatusCheck(GameStateType gameStateType) : base(gameStateType)
    {
    }

    [Inject]
    private void Construct(SignalBus signalBus, TurnManager turnManager, Level level, IPauseHandler pauseHandler, GUIHolder gUIHolder)
    {
        _signalBus = signalBus;
        _turnManager = turnManager;
        _timer = level.Timer;

        _gUIHolder = gUIHolder;

        _pauseHandler = pauseHandler;
    }

    public override bool CanSwitchToState(GameStateType gameStateType) => true;

    public override void OnEnter()
    {
        _timer.TimeIsOver += OnTimeOver;
        _gUIHolder.QuitGame += OnQuit;

        _signalBus.Subscribe<UnitUsedAbilitySignal>(OnUnitUsedAbilitySignal);
        _signalBus.Subscribe<PlayerDeadSignal>(OnPlayerDead);
        _signalBus.Subscribe<PauseGameSignal>(OnPausGame);
    }

    private void OnUnitUsedAbilitySignal(UnitUsedAbilitySignal signal)
    {
        var effects = signal.Effects;

        var effectCommand = new List<Command>();

        var owner = signal.Owner;
        var target = signal.Target;

        foreach (var effect in effects)
        {
            var finalTarget = effect.TargetType.GetTarget(owner, target);
            var command = _turnManager.CreateApplyEffectCommand(effect, finalTarget);
            effectCommand.Add(command);
        }

        var commands = new List<Command>();

        commands.AddRange(effectCommand);

        _turnManager.ExecuteCommands(commands);
    }

    private void OnPlayerDead(PlayerDeadSignal signal)
    {
        var command = _turnManager.CreateSetGameStateCommand(GameStateType.Lose);

        var commands = new List<Command>()
        {
            command,
        };

        _turnManager.ExecuteCommands(commands);

        Cleanup();
    }

    private void OnPausGame(PauseGameSignal signal)
    {
        bool isPaused = signal.IsPaused;

        var command1 = _turnManager.CreateSetPausedGameCommand(isPaused);

        var commands = new List<Command>()
        {
            command1,
        };

        _turnManager.ExecuteCommands(commands);
    }

    private void OnTimeOver()
    {
        var command = _turnManager.CreateSetGameStateCommand(GameStateType.Win);

        var commands = new List<Command>()
        {
            command,
        };

        _turnManager.ExecuteCommands(commands);

        Cleanup();
    }

    private void OnPaused(bool isPaused)
    {
        var command1 = _turnManager.CreateSetPausedGameCommand(isPaused);

        var commands = new List<Command>()
        {
            command1,
        };

        _turnManager.ExecuteCommands(commands);
    }

    private void OnQuit()
    {
        var command1 = _turnManager.CreateQuitGameCommand();

        var commands = new List<Command>()
        {
            command1,
        };

        _turnManager.ExecuteCommands(commands);
    }

    private void Cleanup()
    {
        _signalBus.Unsubscribe<UnitUsedAbilitySignal>(OnUnitUsedAbilitySignal);
        _signalBus.Unsubscribe<PlayerDeadSignal>(OnPlayerDead);
        _signalBus.Unsubscribe<PauseGameSignal>(OnPausGame);

        _timer.TimeIsOver -= OnTimeOver;
        _gUIHolder.QuitGame -= OnQuit;
    }
}
