using System.Collections.Generic;
using UnityEngine;

public abstract class MainMenuPanel<TSlot> : MonoBehaviour where TSlot : MenuSlot
{
    [SerializeField] protected Transform _contentParent;
    [SerializeField] protected TSlot _slotPrefab;

    protected List<TSlot> _slots = new();

    public virtual void Show<T>(IEnumerable<T> objects) where T : IIdentifier
    {
        Clear();

        foreach (var obj in objects)
        {
            var spawnSlot = Instantiate(_slotPrefab, _contentParent);
            //spawnSlot.Init();

            OnShow(spawnSlot, obj.ID);

            _slots.Add(spawnSlot);
        }

        Sort();
    }

    protected abstract void OnShow(TSlot slot, int id);

    public virtual void Sort() { }

    protected virtual void Clear()
    {
        foreach (var slot in _slots)
            Destroy(slot.gameObject);

        _slots.Clear();
    }
}
