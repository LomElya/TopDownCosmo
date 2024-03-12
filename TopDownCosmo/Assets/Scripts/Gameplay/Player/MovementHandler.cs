using System;
using UnityEngine;

public class MovementHandler : IDisposable
{
    private ShipSpawner _spawner;

    private IInput _input;
    private IMovable _movable;
    private IPauseHandler _pauseHandler;

    private bool _isPaused => _pauseHandler.IsPaused;

    public MovementHandler(IInput input, ShipSpawner spawner, IPauseHandler pauseHandler)
    {
        _input = input;
        _pauseHandler = pauseHandler;

        _spawner = spawner;

        _spawner.Spawned += OnSpawn;
        _input.ClickButtonMove += OnClickButtonMove;
    }

    private void OnSpawn(IMovable movable)
    {
        _movable = movable;
    }

    private void OnClickButtonMove(Vector2 direction)
    {
        if (_movable == null || _isPaused)
            return;

        if (OutBorder(_movable.Transform.position, direction))
            return;

        _movable.Move(direction);
    }

    private bool OutBorder(Vector2 position, Vector2 direction)
    {
        var newPosition = position + direction;

        if (newPosition.x < Constants.Border.LEFT_POSITION || newPosition.x > Constants.Border.RIGHT_POSITION)
            return true;

        if (newPosition.y < Constants.Border.DOWN_POSITION || newPosition.y > Constants.Border.UP_POSITION)
            return true;

        return false;
    }


    public void Dispose()
    {
        _spawner.Spawned -= OnSpawn;
        _input.ClickButtonMove -= OnClickButtonMove;
    }
}
