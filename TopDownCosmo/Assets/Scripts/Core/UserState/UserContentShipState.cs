using System;
using System.Collections.Generic;

public sealed class UserContentShipState : ISerializable
{
    public short Version;

    public ShipType Type;
    public List<UserShipState> Ships;

    public void ChangeShip(int id)
    {
        foreach (var ship in Ships)
        {
            ship.Selected = false;

            if (ship.ID == id)
                ship.Selected = true;
        }
    }

    public void OpenShip(int id)
    {
        foreach (var ship in Ships)
        {
            if (ship.ID == id)
                ship.IsShipOpen = true;
        }
    }

    public short GetLenght()
    {
        var lenght = sizeof(short)
            + sizeof(byte)
            + SerializationUtils.GetSizeOfList(Ships);

        return (short)lenght;
    }

    public void Serialize(byte[] data, ref int offset)
    {
        offset += ByteConverter.AddToStream(Version, data, offset);
        offset += ByteConverter.AddToStream((byte)Type, data, offset);

        SerializationUtils.SerializeList(Ships, data, ref offset);
    }

    public void Deserialize(byte[] data, ref int offset)
    {
        offset += ByteConverter.ReturnFromStream(data, offset, out Version);

        offset += ByteConverter.ReturnFromStream(data, offset, out byte type);
        Type = (ShipType)type;

        Ships = SerializationUtils.DeserializeList<UserShipState>(data, ref offset);
    }

    public static UserContentShipState GetInitial(ShipType type)
    {
        return new UserContentShipState()
        {
            Version = 1,
            Type = type,
            Ships = new List<UserShipState>(),
        };
    }
}

public sealed class UserShipState : ISerializable
{
    public short Version;
    public int ID;

    public bool IsShipOpen;
    public bool Selected;

    public short GetLenght()
    {
        var lenght = sizeof(short)
            + sizeof(int)
            + sizeof(byte)
            + sizeof(byte);

        return (short)lenght;
    }

    public void Serialize(byte[] data, ref int offset)
    {
        offset += ByteConverter.AddToStream(Version, data, offset);
        offset += ByteConverter.AddToStream(ID, data, offset);

        offset += ByteConverter.AddToStream(IsShipOpen, data, offset);
        offset += ByteConverter.AddToStream(Selected, data, offset);
    }

    public void Deserialize(byte[] data, ref int offset)
    {
        offset += ByteConverter.ReturnFromStream(data, offset, out Version);
        offset += ByteConverter.ReturnFromStream(data, offset, out ID);

        offset += ByteConverter.ReturnFromStream(data, offset, out IsShipOpen);
        offset += ByteConverter.ReturnFromStream(data, offset, out Selected);
    }

    public static UserShipState GetInitial(int id)
    {
        return new UserShipState()
        {
            Version = 1,
            ID = id,

            IsShipOpen = false,
            Selected = false,
        };
    }
}