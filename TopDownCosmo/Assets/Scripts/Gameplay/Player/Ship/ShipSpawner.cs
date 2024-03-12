using System;
using UnityEngine;
using Zenject;

public class ShipSpawner : MonoBehaviour
{
    public event Action<Ship> Spawned;

    [SerializeField] private Transform _spawnPoint;

    private Ship _currentShip;

    private ShipFactory _factory;

    private UserContainer _userContainer;
    private UserState UserState => _userContainer.State;

    private DiContainer _diContainer;

    [Inject]
    private void Construct(DiContainer diContainer, ShipFactory factory,  UserContainer userContainer)
    {
        _diContainer = diContainer;
        _factory = factory;
        _userContainer = userContainer;
    }

    public void StartGame()
    {
        OnSpawn(UserState.CurrentShipID);
    }

    private void StopGame()
    {

    }

    private void OnSpawn(int id)
    {
        Ship ship = _factory.Get(id, _spawnPoint);
        _diContainer.Inject(ship);

        _currentShip?.Remove();
        _currentShip = ship;

        Spawned?.Invoke(_currentShip);
    }

    private void OnDisable()
    {
        
    }
}
