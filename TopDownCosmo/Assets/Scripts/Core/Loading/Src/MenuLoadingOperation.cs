using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public sealed class MenuLoadingOperation : ILoadingOperation
{
    public string Description => "Main menu loading...";

    public async UniTask Load(Action<float> onProgress)
    {
        float loadingProgress;

        var loadOp = SceneManager.LoadSceneAsync(Constants.Scenes.MENU_SCENE,
            LoadSceneMode.Additive);

        while (loadOp.isDone == false)
        {
            loadingProgress = Mathf.Clamp01(loadOp.progress / 0.9f);
            await UniTask.Yield();
            onProgress?.Invoke(loadingProgress);
        }
    }
}