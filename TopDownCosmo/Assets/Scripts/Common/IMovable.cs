using UnityEngine;

public interface IMovable
{
    float Speed { get; }

    Transform Transform { get; }

    void Move(Vector2 direction);
}
