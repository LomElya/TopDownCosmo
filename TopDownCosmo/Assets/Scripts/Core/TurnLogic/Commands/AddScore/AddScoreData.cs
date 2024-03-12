public class AddScoreData : CommandData
{
    public int Value { get; private set; }

    public AddScoreData(int value)
    {
        Value = value;
    }
}
