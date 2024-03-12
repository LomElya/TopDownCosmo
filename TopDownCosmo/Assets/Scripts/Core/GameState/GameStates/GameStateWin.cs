using System.Collections.Generic;
using Zenject;

public class GameStateWin : GameState
{
    private TurnManager _turnManager;
    private GameResultMenu _gameResultMenu;
    public GameStateWin(GameStateType gameStateType) : base(gameStateType)
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
        var command2 = _turnManager.CreateAddGoldCommand(5);
        var command3 = _turnManager.CreateAddScoreCommand(100);
        var command4 = _turnManager.CreateOpenNextLevelCommand();

        var commands = new List<Command>()
        {
            command1,
            command2,
            command3,
            command4
        };

        _turnManager.ExecuteCommands(commands);

        _gameResultMenu.Show(GameResultType.Victory, OnNextLevel, OnQuit);
    }

    private void OnNextLevel()
    {
        var command1 = _turnManager.CreateNextLevelCommand();
        var command2 = _turnManager.CreateSetGameStateCommand(GameStateType.GameStart);

        var commands = new List<Command>()
        {
            command1,
            command2
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
