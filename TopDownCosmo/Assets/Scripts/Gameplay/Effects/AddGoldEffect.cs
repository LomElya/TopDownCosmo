public class AddGoldEffect : IEffect
{
    private readonly int _addCoinValue;

    private ITriggerable _triggerable;

    public AddGoldEffect(int addCoinValue) => _addCoinValue = addCoinValue;

    public void Assign(ITriggerable triggerable)
    {
        _triggerable = triggerable;

        if (_triggerable.Owner.TryGetComponent(out IPickerCoin pickerCoin))
        {
            pickerCoin.AddGold(_addCoinValue);

            Remove();
        }
    }

    public virtual void Remove() { }
}
