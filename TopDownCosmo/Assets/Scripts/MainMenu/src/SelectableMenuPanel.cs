using System;
using System.Collections.Generic;

public abstract class SelectableMenuPanel<TSlot> : MainMenuPanel<TSlot> where TSlot : SelectableMenuSlot
{
    public event Action<TSlot> SlotViewClicked;

    public override void Show<T>(IEnumerable<T> objects)
    {
        Clear();

        foreach (var obj in objects)
        {
            TSlot spawnSlot = Instantiate(_slotPrefab, _contentParent);
            //spawnSlot.Init();

            spawnSlot.Click += OnClick;

            OnShow(spawnSlot, obj.ID);

            _slots.Add(spawnSlot);
        }

        Sort();
    }

    private void OnClick(SelectableMenuSlot slot) => OnSlotViewClick((TSlot)slot);

    public virtual void Select(TSlot selectedSlot)
    {
        foreach (var slot in _slots)
            slot.UnSelect();

        selectedSlot.Select();
    }

    protected virtual void OnSlotViewClick(TSlot selectedSlot)
    {
        Highlight(selectedSlot);
        SlotViewClick(selectedSlot);
    }

    protected void SlotViewClick(TSlot selectedSlot) => SlotViewClicked?.Invoke(selectedSlot);

    protected override void Clear()
    {
        foreach (var slot in _slots)
        {
            slot.Click -= OnClick;
            Destroy(slot.gameObject);
        }

        _slots.Clear();
    }

    protected virtual void Highlight(TSlot selectedSlot)
    {
        foreach (var slot in _slots)
            slot.UnHighlight();

        selectedSlot.Highlight();
    }
}
