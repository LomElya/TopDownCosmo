using UnityEngine.Events;
using Zenject;

public class NextLevelCommand : Command
{
    private Level _level;

    public NextLevelCommand(NextLevelData data) : base(data)
    {
    }

    [Inject]
    private void Construct(Level level)
    {
        _level = level;
    }

    public override void Execute(UnityAction onCompleted)
    {
        _level.NextLevel();

        onCompleted?.Invoke();
    }
}
