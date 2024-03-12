using System;
using Zenject;

public class SelectLevelPanel : SelectableMenuPanel<SelectLevelSlot>
{
    public event Action<SelectLevelSlot> ItemViewClicked;

    private UserContainer _userContainer;

    [Inject]
    private void Construct(UserContainer userContainer)
    {
        _userContainer = userContainer;
    }

    protected override void OnShow(SelectLevelSlot slot, int id)
    {
        int score = _userContainer.GetLevelScore(id);

        slot.Init(id, score);

        slot.UnSelect();
        slot.UnHighlight();

        if (_userContainer.IsOpenedLevel(id))
        {
            if (_userContainer.IsSelectedLevel(id))
            {
                slot.Highlight();
                slot.Select();
                SlotViewClick(slot);
            }

            slot.UnLock();
        }
        else
            slot.Lock();
    }

    protected override void OnSlotViewClick(SelectLevelSlot selectedSlot)
    {
        if (selectedSlot.IsLock)
            return;

        Select(selectedSlot);

        base.OnSlotViewClick(selectedSlot);
    }
}
