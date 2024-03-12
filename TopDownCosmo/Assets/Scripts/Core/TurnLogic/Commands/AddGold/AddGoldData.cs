public class AddGoldData : CommandData
{
    public int Value { get; private set; }

    public AddGoldData(int value)
    {
        Value = value;
    }
}
