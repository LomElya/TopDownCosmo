using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class GameObjectFactory : ScriptableObject
{
    private Scene _scene;

    private CustomPoolProvider _provider = new();

    protected T CreateGameObjectInstance<T>(T prefab) where T : MonoBehaviour
    {
        if (!_scene.isLoaded)
        {
            if (Application.isEditor)
            {
                _scene = SceneManager.GetSceneByName(name);

                if (!_scene.isLoaded)
                    _scene = SceneManager.CreateScene(name);
            }
            else
                _scene = SceneManager.CreateScene(name);
        }

        T instance = _provider.Get(prefab);

        SceneManager.MoveGameObjectToScene(instance.gameObject, _scene);
        return instance;
    }

    public void Hide<T>(T obj) where T : MonoBehaviour =>
        _provider.Hide(obj);

    public async UniTask Unload()
    {
        if (_scene.isLoaded)
        {
            var unloadOp = SceneManager.UnloadSceneAsync(_scene);

            while (unloadOp.isDone == false)
            {
                await UniTask.Yield();
            }
        }

        _provider.Clear();
    }

}
