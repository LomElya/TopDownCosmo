using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ShopShipSlot : SelectableMenuSlot, IPointerClickHandler
{
    [SerializeField] private Image _shipImage;

    [SerializeField] private StringValueView _priceText;
    [SerializeField] private StringValueView _nameText;

    public int ID { get; private set; }
    public int Price { get; private set; }
    public Sprite Sprite { get; private set; }

    public void Init(ShopShip shopShip)
    {
        Init();

        ID = shopShip.ID;
        Price = shopShip.Price;
        Sprite = shopShip.Image;

        _shipImage.sprite = Sprite;
        _nameText.Show(shopShip.Name);
    }

    public override void Lock()
    {
        base.Lock();

        _priceText.Show(Price.ToString());
    }

    public override void UnLock()
    {
        base.UnLock();

        _priceText.Hide();
    }
}
