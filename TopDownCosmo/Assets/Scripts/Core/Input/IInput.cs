using System;
using UnityEngine;
using Zenject;

public interface IInput: ITickable
{
    event Action<Vector2> ClickButtonMove;

    float Horizontal { get; }
    float Vertical { get; }
    Vector2 Direction { get; }

    bool IsMooving { get; }
}
