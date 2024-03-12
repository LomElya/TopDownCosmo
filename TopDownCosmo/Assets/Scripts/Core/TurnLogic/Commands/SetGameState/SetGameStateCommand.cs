using UnityEngine.Events;
using Zenject;

public class SetGameStateCommand : Command
{
    private Gameplay _gameplay;

    [Inject]
    public void Construct(Gameplay gameplay)
    {
        _gameplay = gameplay;
    }

    public SetGameStateCommand(SetGameStateData data) : base(data)
    {
    }

    public override void Execute(UnityAction onCompleted)
    {
        var setGameStateData = (SetGameStateData)_commandData;
        _gameplay.SetState(setGameStateData.GameStateType);
        onCompleted?.Invoke();
    }
}
