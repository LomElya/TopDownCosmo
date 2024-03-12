using System.Linq;
using UnityEngine;
using Zenject;

public class ShopShipMenu : MainMenu
{
    [Header("Panels")]
    [SerializeField] private ShopShipPanel _shopShipPanel;
    [SerializeField] private ShopShipModelPanel _shopModel;

    [Header("Buttons")]
    [SerializeField] private TypeShipButtonManager _buttonManager;

    private ShopShipContent _shopShipContent;
    private Wallet _wallet;

    private UserContainer _userContainer;
    private UserState UserState => _userContainer.State;
    private IStateCommunicator _stateCommunicator;

    private ShopShipSlot _selectSlot;

    [Inject]
    private void Construct(ShopShipContent shopShipContent, Wallet wallet, UserContainer userContainer, IStateCommunicator stateCommunicator)
    {
        _shopShipContent = shopShipContent;
        _wallet = wallet;

        _userContainer = userContainer;
        _stateCommunicator = stateCommunicator;
    }

    protected override void OnShow()
    {
        _shopShipPanel.SlotViewClicked += OnItemViewClicked;
        _shopShipPanel.Init(_shopShipContent);

        _buttonManager.Init();
        _buttonManager.OnClickButton += OnTypeShipButtonClick;

        _shopModel.BuyButtonClick += OnBuyButtonClicked;
        _shopModel.SelectButtonClick += OnSelectButtonClick;

        OnTypeShipButtonClick(ShipType.BALANCE);
    }

    private void OnTypeShipButtonClick(ShipType type) =>
        _shopShipPanel.Show(_shopShipContent.GetShipContents(type).Contents.Cast<ShopShip>());

    private void OnItemViewClicked(ShopShipSlot slot)
    {
        _selectSlot = slot;

        _shopModel.ShowModel(_selectSlot);
    }

    private void OnBuyButtonClicked(int price)
    {
        _wallet.Spend(price);

        _selectSlot.UnLock();
        _userContainer.OpenShip(_selectSlot.ID);

        OnSelectButtonClick();

        _shopShipPanel.Sort();
    }

    private void OnSelectButtonClick()
    {
        _shopShipPanel.Select(_selectSlot);
        _userContainer.ChangeShip(_selectSlot.ID);

        _stateCommunicator.SaveState(UserState);
    }

    protected override void Disable()
    {
        base.Disable();

        _shopShipPanel.SlotViewClicked -= OnItemViewClicked;

        _shopModel.BuyButtonClick -= OnBuyButtonClicked;
        _shopModel.SelectButtonClick -= OnSelectButtonClick;
    }
}
