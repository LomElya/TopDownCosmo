using System;
using UnityEngine;
using Zenject;

public class ShopShipModelPanel : MonoBehaviour
{
    public event Action<int> BuyButtonClick;
    public event Action SelectButtonClick;

    [SerializeField] private BuyButton _buyButton;
    [SerializeField] private SelectButton _selectButton;
    [SerializeField] private ImagePlacement _placement;

    private ShopShipSlot _selectSlot;

    private Wallet _wallet;

    private void OnEnable()
    {
        _buyButton.Click += OnBuyButtonClick;
        _selectButton.Click += OnSelectButtonClick;
    }

    [Inject]
    private void Construct(Wallet wallet) => _wallet = wallet;

    public void ShowModel(ShopShipSlot slot)
    {
        _selectSlot = slot;

        _placement.InstantiateModel(_selectSlot.Sprite);

        if (_selectSlot.IsLock)
        {
            ShowBuyButton(_selectSlot.Price);
        }
        else
        {
            if (slot.Selected)
                _selectButton.Hide();
            else
                _selectButton.Show();

            _buyButton.Hide();
        }
    }

    private void OnBuyButtonClick()
    {
        _buyButton.Hide();
        BuyButtonClick?.Invoke(_selectSlot.Price);
    }

    private void OnSelectButtonClick()
    {
        _selectButton.Hide();
        SelectButtonClick?.Invoke();
    }

    private void ShowBuyButton(int price)
    {
        _selectButton.Hide();

        _buyButton.UpdateText(price);

        if (_wallet.IsEnought(price))
            _buyButton.Unlock();
        else
            _buyButton.Lock();
    }

    private void OnDisable()
    {
        _buyButton.Click -= OnBuyButtonClick;
        _selectButton.Click -= OnSelectButtonClick;
    }
}
