using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public sealed class GameplayLoadingOperation : ILoadingOperation
{
    public string Description => "Gameplay loading...";

    private AssetProvider _assetProvider;

    public GameplayLoadingOperation(AssetProvider assetProvider) => _assetProvider = assetProvider;

    public async UniTask Load(Action<float> onProgress)
    {
        float loadingProgress;

        var loadOp = SceneManager.LoadSceneAsync(Constants.Scenes.GAME_SCENE, LoadSceneMode.Single);

        while (loadOp.isDone == false)
        {
            loadingProgress = Mathf.Clamp01(loadOp.progress / 0.5f);
            await UniTask.Yield();
            onProgress?.Invoke(loadingProgress);
        }

        var scene = SceneManager.GetSceneByName(Constants.Scenes.GAME_SCENE);
        Gameplay gameplay = scene.GetRoot<Gameplay>();

        onProgress?.Invoke(0.8f);

        gameplay.Init();

        onProgress?.Invoke(1f);
    }
}