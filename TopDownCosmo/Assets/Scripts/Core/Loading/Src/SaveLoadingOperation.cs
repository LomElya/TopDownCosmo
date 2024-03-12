using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

public sealed class SaveLoadingOperation : ILoadingOperation
{
    public string Description => "Save loading...";

    private readonly UserContainer _userContainer;
    private readonly IStateCommunicator _stateCommunicator;

    private Action<float> _onProgress;

    public SaveLoadingOperation(UserContainer userContainer, IStateCommunicator stateCommunicator)
    {
        _userContainer = userContainer;
        _stateCommunicator = stateCommunicator;
    }

    public async UniTask Load(Action<float> onProgress)
    {
        _onProgress = onProgress;

        _onProgress.Invoke(0.3f);

        _userContainer.SetState(await GetState());

        onProgress?.Invoke(1f);
    }

    private async UniTask<UserState> GetState()
    {
        var result = await _stateCommunicator.GetState();
        _onProgress.Invoke(0.6f);

        if (result.IsValid() == false)
        {
            result = UserState.GetInitial();
            await _stateCommunicator.SaveState(result);
        }
        else
            result.IsFirstStart = false;

        return result;
    }
}