using System;
using System.Collections.Generic;
using UnityEngine;


public sealed class UserSurviveScenario : ISerializable
{
    public short Version;

    public int CurrentLevelID;

    public List<UserLevelState> Levels;

    public void ChangeLevel(int id)
    {
        CurrentLevelID = id;

        foreach (var level in Levels)
            level.Selected = false;

        Levels[CurrentLevelID].Selected = true;
    }

    public void OpenLevel(int id) =>
        Levels[id].IsLevelOpen = true;

    public short GetLenght()
    {
        var lenght = sizeof(short)
            + sizeof(int)
            + sizeof(long)
            + SerializationUtils.GetSizeOfList(Levels);

        return (short)lenght;
    }

    public void Serialize(byte[] data, ref int offset)
    {
        offset += ByteConverter.AddToStream(Version, data, offset);
        offset += ByteConverter.AddToStream(CurrentLevelID, data, offset);

        SerializationUtils.SerializeList(Levels, data, ref offset);
    }

    public void Deserialize(byte[] data, ref int offset)
    {
        offset += ByteConverter.ReturnFromStream(data, offset, out Version);
        offset += ByteConverter.ReturnFromStream(data, offset, out CurrentLevelID);

        Levels = SerializationUtils.DeserializeList<UserLevelState>(data, ref offset);
    }

    public static UserSurviveScenario GetInitial()
    {
        int startLevelID = 0;

        return new UserSurviveScenario()
        {
            Version = 1,
            CurrentLevelID = startLevelID,
            Levels = new List<UserLevelState>(),
        };
    }
}

