public class SetPausedGameData : CommandData
{
    public bool IsPaused { get; private set; }

    public SetPausedGameData(bool isPaused) => IsPaused = isPaused;
}
