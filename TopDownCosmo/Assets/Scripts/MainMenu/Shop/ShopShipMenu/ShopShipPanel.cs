using System;
using System.Linq;
using UnityEngine;
using Zenject;

public class ShopShipPanel : SelectableMenuPanel<ShopShipSlot>
{
    private ShopShipContent _shopShipContent;
    private ShopShip _currentConfigShip;

    private UserContainer _userContainer;

    [Inject]
    private void Construct(UserContainer userContainer)
    {
        _userContainer = userContainer;
    }

    public void Init(ShopShipContent shipContent)
    {
        _shopShipContent = shipContent;
    }

    protected override void OnShow(ShopShipSlot slot, int id)
    {
        ShopShip ship = _shopShipContent.GetShipConfig(id);

        slot.Init(ship);

        slot.UnSelect();
        slot.UnHighlight();

        if (_userContainer.IsOppenedShip(id))
        {
            if (_userContainer.IsSelectedShip(id))
            {
                slot.Highlight();
                slot.Select();
                OnSlotViewClick(slot);
            }

            slot.UnLock();
        }
        else
            slot.Lock();
    }

    public override void Sort()
    {
        _slots = _slots
           .OrderBy(item => item.IsLock)
           .ThenByDescending(item => item.Price * -1)
           .ToList();

        for (int i = 0; i < _slots.Count; i++)
            _slots[i].transform.SetSiblingIndex(i);
    }
}
