using UnityEngine.Events;
using Zenject;

public class OpenNextLevelCommand : Command
{
    private Level _level;

    public OpenNextLevelCommand(OpenNextLevelData data) : base(data)
    {
    }

    [Inject]
    private void Construct(Level level)
    {
        _level = level;
    }

    public override void Execute(UnityAction onCompleted)
    {
        _level.OpenLevel();

        onCompleted?.Invoke();
    }
}
