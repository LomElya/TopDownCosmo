using UnityEngine;

public class WalletHolder : MonoBehaviour, IPickerCoin
{
    private Wallet _wallet;

    public GameplayObject Owner { get; private set; }

    public EffectMediator EffectMediator { get; private set; }

    public void Init(GameplayObject owner, Wallet wallet)
    {
        Owner = owner;
        _wallet = wallet;

        EffectMediator = new EffectMediator(this);
    }
    public void AddGold(int value) => _wallet.AddGold(value);
}
