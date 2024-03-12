public class GameProcessorData : CommandData
{
    public UserLevelState LevelState { get; private set; }

    public GameProcessorData(UserLevelState levelState) => LevelState = levelState;
}
