using System.Collections.Generic;
using Zenject;

public class GameStateLose : GameState
{
    private TurnManager _turnManager;
    private GameResultMenu _gameResultMenu;

    public GameStateLose(GameStateType gameStateType) : base(gameStateType)
    {
    }

    [Inject]
    private void Construct(TurnManager turnManager, GameResultMenu gameResultMenu)
    {
        _turnManager = turnManager;
        _gameResultMenu = gameResultMenu;
    }

    public override bool CanSwitchToState(GameStateType gameStateType) => true;

    public override void OnEnter()
    {
        var command1 = _turnManager.CreateSetPausedGameCommand(true);

        var commands = new List<Command>()
        {
            command1,
        };

        _turnManager.ExecuteCommands(commands);

        _gameResultMenu.Show(GameResultType.Defeat, OnRestartLevel, OnQuit);
    }

    private void OnRestartLevel()
    {
        var command1 = _turnManager.CreateSetGameStateCommand(GameStateType.GameStart);

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
}
