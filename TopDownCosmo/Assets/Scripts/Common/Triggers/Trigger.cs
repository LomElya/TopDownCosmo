using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class Trigger<T> : MonoBehaviour where T : ITriggerable
{
    public event Action<T> Enter;

    private Collider2D _collider;

    private void OnValidate()
    {
        _collider ??= GetComponent<Collider2D>();
        _collider.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out T triggered))
        {
            Enter?.Invoke(triggered);

            OnEnter(triggered);
        }
    }

    protected virtual void OnEnter(T triggered) { }
}
