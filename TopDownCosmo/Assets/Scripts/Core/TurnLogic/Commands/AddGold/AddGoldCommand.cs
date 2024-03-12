using UnityEngine.Events;
using Zenject;

public class AddGoldCommand : Command
{
    private Wallet _wallet;

    public AddGoldCommand(AddGoldData data) : base(data)
    {
    }

    [Inject]
    private void Construct(Wallet wallet)
    {
        _wallet = wallet;
    }

    public override void Execute(UnityAction onCompleted)
    {
        var data = (AddGoldData)_commandData;
        int value = data.Value;
        
        _wallet.AddGold(value);

        onCompleted?.Invoke();
    }
}
