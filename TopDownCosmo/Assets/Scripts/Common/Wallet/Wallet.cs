using System;

public class Wallet
{
    public event Action<int> GoldChange;

    private IStateCommunicator _stateCommunicator;
    private UserContainer _userContainer;
    private UserState _userState => _userContainer.State;

    private int _profitToLevel = 0;

    public Wallet(IStateCommunicator stateCommunicator, UserContainer userContainer)
    {
        _stateCommunicator = stateCommunicator;
        _userContainer = userContainer;
    }

    public void StartLevel()
    {
        _profitToLevel = 0;
    }

    public void AddGold(int value)
    {
        _profitToLevel =+ value;

        _userContainer.AddGold(value);

        OnChangeGold();
    }

    public void Spend(int value)
    {
        _profitToLevel =- value;

        _userContainer.SpendGold(value);

        OnChangeGold();
    }

    private void OnChangeGold()
    {
        GoldChange?.Invoke(GetCurrentGold());
        _stateCommunicator.SaveState(_userState);
    }

    public int GetCurrentGold() => _userContainer.GetCurrentGold();
    public int GetLevelGold() => _profitToLevel;

    public bool IsEnought(int value) => _userContainer.IsEnought(value);
}
