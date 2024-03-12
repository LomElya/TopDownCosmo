using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameStateStart : GameState
{
    private UserContainer _userContainer;
    private TurnManager _turnManager;

    public GameStateStart(GameStateType gameStateType) : base(gameStateType) { }

    [Inject]
    private void Construct(UserContainer userContainer, TurnManager turnManager)
    {
        _userContainer = userContainer;
        _turnManager = turnManager;

    }

    public override bool CanSwitchToState(GameStateType gameStateType) => true;

    public override void OnEnter()
    {
        var command1 = _turnManager.CreateStartGameProcessorsCommand(_userContainer.GetLevelState());

        var command2 = _turnManager.CreateSetPausedGameCommand(false);

        var command3 = _turnManager.CreateSetGameStateCommand(GameStateType.GameStatusCheck);

        var commands = new List<Command>()
        {
            command1,
            command2,
            command3,
        };

        _turnManager.ExecuteCommands(commands);
    }
}
