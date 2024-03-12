using Zenject;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class AppStartup : MonoBehaviour
{
    private LoadingScreenProvider _loadingProvider;
    private AssetProvider _assetProvider;

    private UserContainer _userContainer;
    private IStateCommunicator _stateCommunicator;

    private LevelConfig _levelConfig;
    private ShopShipContent _shipContent;

    [Inject]
    private void Construct(LoadingScreenProvider loadingProvider, AssetProvider assetProvider, UserContainer userContainer, IStateCommunicator stateCommunicator, LevelConfig levelConfig, ShopShipContent shipContent)
    {
        _loadingProvider = loadingProvider;
        _assetProvider = assetProvider;

        _userContainer = userContainer;
        _stateCommunicator = stateCommunicator;
        _levelConfig = levelConfig;
        _shipContent = shipContent;
    }

    private void Start()
    {
        var loadingOperations = new Queue<ILoadingOperation>();

        loadingOperations.Enqueue(_assetProvider);
        loadingOperations.Enqueue(new SaveLoadingOperation(_userContainer, _stateCommunicator));
        loadingOperations.Enqueue(new ConfigOperation(_levelConfig, _shipContent, _stateCommunicator, _userContainer));
        loadingOperations.Enqueue(new MenuLoadingOperation());

        _loadingProvider.LoadAndDestroy(loadingOperations).Forget();
    }
}
