using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(Canvas))]
public class GameResultMenu : MonoBehaviour
{
    [SerializeField] private GameResultIntroAnimation _introAnimation;

    [Header("Buttons")]
    [SerializeField] private Button _nextLevelButton;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _quitButton;

    [Header("Text")]
    [SerializeField] private StringValueView _scoreLevel;
    [SerializeField] private StringValueView _maxScoreLevel;
    [SerializeField] private IntValueView _profit;

    private Wallet _wallet;
    private Score _score;
    private Level _level;

    private Canvas _canvas;
    private Camera _mainCamera => Camera.main;

    private Action _onQuit;
    private Action _callback;

    private void OnValidate()
    {
        if (_canvas == null)
            _canvas = GetComponent<Canvas>();
    }

    [Inject]
    private void Construct(Wallet wallet, Score score, Level level)
    {
        _wallet = wallet;
        _score = score;
        _level = level;
    }

    private void Awake()
    {
        _canvas.enabled = false;

        _nextLevelButton.onClick.AddListener(OnNextLevelButtonClick);
        _restartButton.onClick.AddListener(OnRestartButtonClick);
        _quitButton.onClick.AddListener(OnQuitButtonClick);
    }

    public async void Show(GameResultType result, Action callback, Action onQuit)
    {
        _callback = callback;
        _onQuit = onQuit;

        ShowInformationLevel();

        OnInteractButton(false);

        _canvas.enabled = true;
        await _introAnimation.Play(result);

        OnInteractButton(true);
    }

    private void ShowInformationLevel()
    {
        _scoreLevel.Show(_score.GetCurrentScore().ToString());
        _maxScoreLevel.Show(_score.GetTotalLeveltScore().ToString());
        _profit.Show(_wallet.GetLevelGold());
    }

    private void OnInteractButton(bool isInteractable)
    {
        _nextLevelButton.interactable = isInteractable;
        _restartButton.interactable = isInteractable;
        _quitButton.interactable = isInteractable;
    }

    private void OnNextLevelButtonClick()
    {
        _callback?.Invoke();
        _canvas.enabled = false;
    }

    private void OnRestartButtonClick()
    {
        _callback?.Invoke();
        _canvas.enabled = false;
    }

    private void OnQuitButtonClick()
    {
        _onQuit?.Invoke();
        _canvas.enabled = false;
    }

    private void OnDisable()
    {
        _nextLevelButton.onClick.RemoveListener(OnNextLevelButtonClick);
        _restartButton.onClick.RemoveListener(OnRestartButtonClick);
        _quitButton.onClick.RemoveListener(OnQuitButtonClick);
    }
}
