using System;
using System.Collections.Generic;

public sealed class UserLevelState : ISerializable
{
    public short Version;

    public int ID;

    public int Score;

    public int GoldForPass;
    public float LevelLength;
    public float StartSpeed;
    public float EndSpeed;

    public bool IsLevelOpen;
    public bool Selected;

    public List<InteractableSpawnState> InteractableSpawn;

    public short GetLenght()
    {
        var lenght = sizeof(short)
            + sizeof(int)
            + sizeof(int)
            + sizeof(int)
            + sizeof(float)
            + sizeof(float)
            + sizeof(float)
            + sizeof(byte)
            + sizeof(byte)
            + SerializationUtils.GetSizeOfList(InteractableSpawn);

        return (short)lenght;
    }

    public void Serialize(byte[] data, ref int offset)
    {
        offset += ByteConverter.AddToStream(Version, data, offset);
        offset += ByteConverter.AddToStream(ID, data, offset);
        offset += ByteConverter.AddToStream(Score, data, offset);
        offset += ByteConverter.AddToStream(GoldForPass, data, offset);

        offset += ByteConverter.AddToStream(LevelLength, data, offset);
        offset += ByteConverter.AddToStream(StartSpeed, data, offset);
        offset += ByteConverter.AddToStream(EndSpeed, data, offset);


        offset += ByteConverter.AddToStream(IsLevelOpen, data, offset);
        offset += ByteConverter.AddToStream(Selected, data, offset);

        SerializationUtils.SerializeList(InteractableSpawn, data, ref offset);
    }

    public void Deserialize(byte[] data, ref int offset)
    {
        offset += ByteConverter.ReturnFromStream(data, offset, out Version);
        offset += ByteConverter.ReturnFromStream(data, offset, out ID);
        offset += ByteConverter.ReturnFromStream(data, offset, out Score);
        offset += ByteConverter.ReturnFromStream(data, offset, out GoldForPass);

        offset += ByteConverter.ReturnFromStream(data, offset, out LevelLength);
        offset += ByteConverter.ReturnFromStream(data, offset, out StartSpeed);
        offset += ByteConverter.ReturnFromStream(data, offset, out EndSpeed);

        offset += ByteConverter.ReturnFromStream(data, offset, out IsLevelOpen);
        offset += ByteConverter.ReturnFromStream(data, offset, out Selected);

        InteractableSpawn = SerializationUtils.DeserializeList<InteractableSpawnState>(data, ref offset);
    }

    public static UserLevelState GetInitial(LevelData levelData)
    {
        var interavtableLevelStates = new List<InteractableSpawnState>();

        foreach (var interactableSpawn in levelData.InteractableSpawn)
            interavtableLevelStates.Add(InteractableSpawnState.GetInitial(interactableSpawn));

        return new UserLevelState()
        {
            Version = 1,
            ID = levelData.ID,

            Score = 0,

            GoldForPass = levelData.GoldForPass,
            LevelLength = levelData.LevelLength,
            StartSpeed = levelData.StartSpeed,
            EndSpeed = levelData.EndSpeed,

            IsLevelOpen = false,
            Selected = false,

            InteractableSpawn = interavtableLevelStates,
        };
    }
}

public sealed class InteractableSpawnState : ISerializable
{
    public InteractableType InteractableType;

    public float StartCooldown;
    public float EndCooldown;
    public float PrewarmTime;

    public short GetLenght()
    {
        var lenght = sizeof(byte)
            + sizeof(float)
            + sizeof(float)
            + sizeof(float);

        return (short)lenght;
    }

    public void Serialize(byte[] data, ref int offset)
    {
        offset += ByteConverter.AddToStream((byte)InteractableType, data, offset);

        offset += ByteConverter.AddToStream(StartCooldown, data, offset);
        offset += ByteConverter.AddToStream(EndCooldown, data, offset);
        offset += ByteConverter.AddToStream(PrewarmTime, data, offset);
    }

    public void Deserialize(byte[] data, ref int offset)
    {
        offset += ByteConverter.ReturnFromStream(data, offset, out byte interactableType);
        InteractableType = (InteractableType)interactableType;

        offset += ByteConverter.ReturnFromStream(data, offset, out StartCooldown);
        offset += ByteConverter.ReturnFromStream(data, offset, out EndCooldown);
        offset += ByteConverter.ReturnFromStream(data, offset, out PrewarmTime);

    }

    public static InteractableSpawnState GetInitial(InteractableSpawnData spawnData)
    {
        return new InteractableSpawnState()
        {
            InteractableType = spawnData.Type,

            StartCooldown = spawnData.StartCooldown,
            EndCooldown = spawnData.EndCooldown,
            PrewarmTime = spawnData.PrewarmTime
        };
    }
}