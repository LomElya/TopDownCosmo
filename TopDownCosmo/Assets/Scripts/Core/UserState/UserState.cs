using System;
using System.Collections.Generic;
using System.Linq;

public sealed class UserState : ISerializable
{
    public short Version;
    public int ID;

    public int CurrentShipID;

    public bool IsFirstStart;

    public UserCurrenciesState Currencies;
    public List<UserContentShipState> ContentShips;

    public UserSurviveScenario SurviveScenario;

    public void ChangeShip(int id, ShipType type)
    {
        CurrentShipID = id;

        var contentShip = ContentShips.FirstOrDefault(x => x.Type == type);

        foreach (var contentShip2 in ContentShips)
        {
            foreach (var ship in contentShip2.Ships)
            {
                if (ship.ID == id)
                {
                    contentShip2.ChangeShip(id);
                }
            }
        }

        contentShip.ChangeShip(id);
    }

    public void ChangeShip(int id)
    {
        CurrentShipID = id;

        foreach (var contentShip in ContentShips)
        {
            foreach (var ship in contentShip.Ships)
            {
                ship.Selected = false;

                if (ship.ID == id)
                {
                    contentShip.ChangeShip(id);
                }
            }
        }
    }

    public void OpenShip(int id, ShipType type)
    {
        var contentShip = ContentShips.FirstOrDefault(x => x.Type == type);
        contentShip.OpenShip(id);
    }

    public void OpenShip(int id)
    {
        foreach (var contentShip in ContentShips)
        {
            foreach (var ship in contentShip.Ships)
            {
                if (ship.ID == id)
                {
                    contentShip.OpenShip(id);
                }
            }
        }
    }

    public short GetLenght()
    {
        var lenght = sizeof(short)
            + sizeof(int)
            + sizeof(int)
            + sizeof(byte)
            + Currencies.GetLenght()
             + SerializationUtils.GetSizeOfList(ContentShips)
            + SurviveScenario.GetLenght();

        return (short)lenght;
    }

    public void Serialize(byte[] data, ref int offset)
    {
        offset += ByteConverter.AddToStream(Version, data, offset);
        offset += ByteConverter.AddToStream(ID, data, offset);
        offset += ByteConverter.AddToStream(CurrentShipID, data, offset);

        offset += ByteConverter.AddToStream(IsFirstStart, data, offset);

        SerializationUtils.SerializeList(ContentShips, data, ref offset);

        Currencies.Serialize(data, ref offset);
        SurviveScenario.Serialize(data, ref offset);
    }

    public void Deserialize(byte[] data, ref int offset)
    {
        offset += ByteConverter.ReturnFromStream(data, offset, out Version);
        offset += ByteConverter.ReturnFromStream(data, offset, out ID);
        offset += ByteConverter.ReturnFromStream(data, offset, out CurrentShipID);

        offset += ByteConverter.ReturnFromStream(data, offset, out IsFirstStart);

        ContentShips = SerializationUtils.DeserializeList<UserContentShipState>(data, ref offset);

        Currencies = SerializationUtils.Deserialize<UserCurrenciesState>(data, ref offset);
        SurviveScenario = SerializationUtils.Deserialize<UserSurviveScenario>(data, ref offset);
    }

    public static UserState GetInitial()
    {
        var rnd = new Random();
        return new UserState()
        {
            Version = 1,

            ID = rnd.Next(1, int.MaxValue),

            CurrentShipID = -1,

            IsFirstStart = true,

            Currencies = UserCurrenciesState.GetInitial(),
            ContentShips = new List<UserContentShipState>(),

            SurviveScenario = UserSurviveScenario.GetInitial(),
        };
    }
}