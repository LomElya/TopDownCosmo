using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEngine.UI;

[RequireComponent(typeof(Canvas))]
public abstract class MainMenu : MonoBehaviour
{
    [SerializeField] Button _backButton;

    private Canvas _canvas;
    private Camera _mainCamera => Camera.main;

    protected UniTaskCompletionSource _showCompletion;

    private void OnValidate()
    {
        if (_canvas == null)
            _canvas = GetComponent<Canvas>();
    }

    public UniTask Show()
    {
        _showCompletion = new UniTaskCompletionSource();

        OnShow();

        _canvas.worldCamera = _mainCamera;
        _canvas.enabled = true;

        return _showCompletion.Task;
    }

    protected abstract void OnShow();

    protected virtual void Hide()
    {
        _canvas.enabled = false;
        _showCompletion?.TrySetResult();
    }

    private void OnEnable() => Enable();
    private void OnDisable() => Disable();

    protected virtual void Enable() => _backButton.onClick.AddListener(Hide);
    protected virtual void Disable() => _backButton.onClick.AddListener(Hide);
}
