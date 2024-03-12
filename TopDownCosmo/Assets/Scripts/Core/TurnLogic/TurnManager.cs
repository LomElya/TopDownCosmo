using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class TurnManager
{
    private IEnumerator _executeCommand;

    private MonoBehaviour _context;

    private DiContainer _container;

    public TurnManager(MonoBehaviour context) => _context = context;

    [Inject]
    private void Construct(DiContainer container)
    {
        _container = container;
    }

    public void ExecuteCommands(List<Command> commands)
    {
        //TODO - добавить проверку на то что корутина уже запущена...

        _executeCommand = ExecuteCommandsCoroutine(commands);

        _context.StartCoroutine(_executeCommand);
    }

    public Command CreateSetGameStateCommand(GameStateType gameStateType)
    {
        var command = new SetGameStateCommand(new SetGameStateData(gameStateType));
        _container.Inject(command);
        return command;
    }

    public Command CreateStartGameProcessorsCommand(UserLevelState levelState)
    {
        var command = new GameProcessorCommand(new GameProcessorData(levelState));
        _container.Inject(command);
        return command;
    }

    public Command CreateSetPausedGameCommand(bool isPaused)
    {
        var command = new SetPausedGameCommand(new SetPausedGameData(isPaused));
        _container.Inject(command);
        return command;
    }

    public Command CreateOpenNextLevelCommand()
    {
        var command = new OpenNextLevelCommand(new OpenNextLevelData());
        _container.Inject(command);
        return command;
    }

    public Command CreateNextLevelCommand()
    {
        var command = new NextLevelCommand(new NextLevelData());
        _container.Inject(command);
        return command;
    }

    public Command CreateAddGoldCommand(int value)
    {
        var command = new AddGoldCommand(new AddGoldData(value));
        _container.Inject(command);
        return command;
    }

    public Command CreateAddScoreCommand(int value)
    {
        var command = new AddScoreCommand(new AddScoreData(value));
        _container.Inject(command);
        return command;
    }

    public Command CreateQuitGameCommand()
    {
        var command = new QuitGameCommand(new QuitGameData());
        _container.Inject(command);
        return command;
    }

    public Command CreateApplyEffectCommand(EffectModel effectModel, ITriggerable triggerable)
    {
        var command = new ApplyEffectCommand(new ApplyEffectData(effectModel, triggerable));
        _container.Inject(command);
        return command;
    }

    public Command CreateLoadSceneCommand(string sceneName)
    {
        var command = new LoadSceneCommand(new LoadSceneData(sceneName));
        _container.Inject(command);
        return command;
    }

    private IEnumerator ExecuteCommandsCoroutine(List<Command> commands)
    {
        foreach (var command in commands)
        {
            bool commandCompleted = false;
            command.Execute(() =>
            {
                commandCompleted = true;
            });
            yield return new WaitUntil(() => commandCompleted == true);
        }
    }
}
