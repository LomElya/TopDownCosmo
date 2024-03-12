using System;
using System.Collections.Generic;
using UnityEngine;

public class TileMover : MonoBehaviour, IDisposable
{
    [SerializeField] private List<ImageScroller> _images = new List<ImageScroller>();

    private float _speedMove = 1f;

    private bool _isPaused = true;

    public void Init(UserLevelState userLevelState)
    {
        _speedMove = userLevelState.StartSpeed;
    }

    public void StartMove() => _isPaused = false;

    public void Stop() => _isPaused = true;
    public void SetPaused(bool isPaused) => _isPaused = isPaused;

    private void Update()
    {
        if (_isPaused)
            return;

        Move();
    }

    public void Move()
    {
        foreach (var image in _images)
        {
            image.Scrolling(_speedMove);
        }
    }

    public void Dispose()
    {
        /*   _game.ChangeGameStatus -= ChangeGameStatus;
          _level.LevelSeted -= OnLevelSet; */
    }
}
