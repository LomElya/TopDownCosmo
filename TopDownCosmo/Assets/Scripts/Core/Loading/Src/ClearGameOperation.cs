using System;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

public sealed class ClearGameOperation : ILoadingOperation
{
    public string Description => "Clearing...";

    private readonly IGameModeCleaner _gameCleanUp;

    public ClearGameOperation(IGameModeCleaner gameCleanUp)
    {
        _gameCleanUp = gameCleanUp;
    }

    public async UniTask Load(Action<float> onProgress)
    {
        onProgress?.Invoke(0.2f);
        _gameCleanUp.Cleanup();

        foreach (var factory in _gameCleanUp.Factories)
            await factory.Unload();

        onProgress?.Invoke(0.5f);

        await LoadMenuOperation();

        onProgress?.Invoke(0.75f);

        await UnLoadSceneOperation();

        onProgress?.Invoke(1f);
    }

    private async UniTask LoadMenuOperation()
    {
        var loadOperation = SceneManager.LoadSceneAsync(Constants.Scenes.MENU_SCENE, LoadSceneMode.Additive);

        while (loadOperation.isDone == false)
            await UniTask.Yield();
    }

    private async UniTask UnLoadSceneOperation()
    {
        var unloadOperation = SceneManager.UnloadSceneAsync(_gameCleanUp.SceneName);

        while (unloadOperation.isDone == false)
            await UniTask.Yield();
    }
}