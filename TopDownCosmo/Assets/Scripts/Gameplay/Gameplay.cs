using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Gameplay : MonoBehaviour, IGameModeCleaner
{
    public string SceneName => Constants.Scenes.GAME_SCENE;

    private GameState _currentGameState;
    private GameStateFabric _fabric;

    public GameStateType PrevGameState { get; private set; }

    private Dictionary<GameStateType, GameState> _states = new();

    private InteractableFactory _interactableFactory;
    private ShipFactory _shipFactory;

    public IEnumerable<GameObjectFactory> Factories => new GameObjectFactory[]
    {
        _interactableFactory, _shipFactory
    };

    [Inject]
    private void Construct(GameStateFabric fabric,
        InteractableFactory interactableFactory, ShipFactory shipFactory)
    {
        _interactableFactory = interactableFactory;

        _shipFactory = shipFactory;

        _fabric = fabric;
    }

    public void Init()
    {
        _currentGameState = _fabric.CreateGameState(GameStateType.GameStart);
        PrevGameState = GameStateType.GameStart;
        _currentGameState.OnEnter();
    }

    public void SetState(GameStateType gameStateType)
    {
        if (_currentGameState.CanSwitchToState(gameStateType))
        {
            GameState newGameState;

            if (_states.ContainsKey(gameStateType))
                newGameState = _states[gameStateType];
            else
            {
                newGameState = _fabric.CreateGameState(gameStateType);
                _states.Add(gameStateType, newGameState);
            }

            PrevGameState = _currentGameState.GameStateType;
            _currentGameState = newGameState;
            newGameState.OnEnter();
        }
    }

    public void Cleanup()
    {

    }
}
