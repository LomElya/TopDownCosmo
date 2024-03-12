using Cysharp.Threading.Tasks;
using System;
using System.IO;
using UnityEngine;

public sealed class LocalStateCommunicator : IStateCommunicator
{
    private const string FileName = "userState";
    private const string SaveFileExtension = ".def";

    private string SavePath => Application.persistentDataPath;
    private string FullPath => Path.Combine(SavePath, $"{FileName}{SaveFileExtension}");

    public UniTask<bool> SaveState(UserState userState)
    {
        var offset = 0;
        var lenght = userState.GetLenght();
        var writeBytes = new byte[lenght];

        userState.Serialize(writeBytes, ref offset);

        File.WriteAllBytes(FullPath, writeBytes);

        return UniTask.FromResult(true);
    }

    public async UniTask<UserState> GetState()
    {
        var result = new UserState();
        var path = FullPath;

        if (IsDataAlreadyExist())
        {
            var offset = 0;
            result.Deserialize(await ReadBytes(), ref offset);
        }

        await UniTask.Delay(TimeSpan.FromSeconds(0.5f));

        return result;
    }

    public async UniTask<byte[]> ReadBytes() => await File.ReadAllBytesAsync(FullPath);

    private bool IsDataAlreadyExist() => File.Exists(FullPath);
}