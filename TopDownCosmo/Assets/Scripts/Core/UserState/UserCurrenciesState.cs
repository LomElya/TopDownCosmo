using System;

public sealed class UserCurrenciesState : ISerializable
{
    public short Version;
    public int Gold;

    public event Action<int> ChangedGold;

    public void AddGold(int value)
    {
        Gold += value;
        ChangedGold?.Invoke(Gold);
    }

    public void SpendGold(int value)
    {
        Gold -= value;
        ChangedGold?.Invoke(Gold);
    }

    public short GetLenght()
    {
        var lenght = sizeof(short)
            + sizeof(int);

        return (short)lenght;
    }

    public void Serialize(byte[] data, ref int offset)
    {
        offset += ByteConverter.AddToStream(Version, data, offset);
        offset += ByteConverter.AddToStream(Gold, data, offset);
    }

    public void Deserialize(byte[] data, ref int offset)
    {
        offset += ByteConverter.ReturnFromStream(data, offset, out Version);
        offset += ByteConverter.ReturnFromStream(data, offset, out Gold);
    }

    public static UserCurrenciesState GetInitial()
    {
        return new UserCurrenciesState()
        {
            Version = 1,
            Gold = 2000
        };
    }
}