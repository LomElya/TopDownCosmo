using Cysharp.Threading.Tasks;
using System;
using System.Linq;
using UnityEngine;

public sealed class ConfigOperation : ILoadingOperation
{
    public string Description => "Configuration loading...";

    private readonly UserContainer _userContainer;
    private readonly IStateCommunicator _stateCommunicator;

    private readonly LevelConfig _levelConfig;
    private readonly ShopShipContent _shipContent;

    private float _countLoadOperation;

    private float _loadingProgress;
    private float _progress;

    private Action<float> _onProgress;

    public ConfigOperation(LevelConfig levelConfig, ShopShipContent shipContent, IStateCommunicator stateCommunicator, UserContainer userContainer)
    {
        _levelConfig = levelConfig;
        _shipContent = shipContent;

        _stateCommunicator = stateCommunicator;
        _userContainer = userContainer;

        foreach (LevelData levelData in _levelConfig.LevelData)
        {
            _countLoadOperation++;
            foreach (var interactableSpawn in levelData.InteractableSpawn)

                _countLoadOperation++;
        }

        foreach (ShipContent contentShip in _shipContent.Contents)
        {
            _countLoadOperation++;
            foreach (ShopShip shipConfig in contentShip.Contents)
                _countLoadOperation++;
        }
    }

    public async UniTask Load(Action<float> onProgress)
    {
        _onProgress = onProgress;

        await GetConfigState();

        _onProgress?.Invoke(1f);
    }

    private async UniTask GetConfigState()
    {
        UserState state = _userContainer.State;

        await GetConfigLevelState(state);
        await GetConfigShipState(state);

        await _stateCommunicator.SaveState(state);

        _onProgress.Invoke(1f);
    }

    private async UniTask GetConfigLevelState(UserState state)
    {
        await UniTask.WaitForSeconds(0.1f);

        UserLevelState levelState;
        // InteractableSpawnState spawnState;

        foreach (LevelData levelData in _levelConfig.LevelData)
        {
            levelState = UserLevelState.GetInitial(levelData);
            state.AddOrReplaceLevel(levelState);
        }
    }

    private async UniTask GetConfigShipState(UserState state)
    {
        await UniTask.WaitForSeconds(0.1f);

        UserContentShipState contentState;
        UserShipState shipState;

        foreach (ShipContent shipContent in _shipContent.Contents)
        {
            contentState = UserContentShipState.GetInitial(shipContent.Type);
            state.AddOrReplaceContentShip(contentState);

            foreach (ShopShip shipConfig in shipContent.Contents)
            {
                shipState = UserShipState.GetInitial(shipConfig.ID);
                contentState.AddOrReplaceShip(shipState);

                _loadingProgress = Mathf.Clamp01(_progress / _countLoadOperation);
                _onProgress?.Invoke(_loadingProgress);
                _progress++;
            }
        }

        if (state.CurrentShipID == -1)
        {
            state.ChangeShip(0);
            state.OpenShip(0);
        }
    }
}
