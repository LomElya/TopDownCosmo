public class SetGameStateData : CommandData
{  
    public GameStateType GameStateType {get; private set;}

    public SetGameStateData(GameStateType gameStateType)
    {
        GameStateType = gameStateType;
    }
}
