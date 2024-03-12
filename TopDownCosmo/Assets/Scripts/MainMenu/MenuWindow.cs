using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(Canvas))]
public class MenuWindow : MonoBehaviour
{
    [SerializeField] private MenuButtonManager _buttonManager;

    [Header("Buttons")]
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _exitButton;

    private AssetProvider _assetProvider;
    private LoadingScreenProvider _loadingProvider;

    private Camera _mainCamera => Camera.main;
    private Canvas _canvas;

    [Inject]
    private void Construct(AssetProvider assetProvider, LoadingScreenProvider loadingProvider)
    {
        _assetProvider = assetProvider;
        _loadingProvider = loadingProvider;
    }

    private void OnValidate()
    {
        _canvas ??= GetComponent<Canvas>();
    }

    private void Start()
    {
        _canvas.worldCamera = _mainCamera;

        _playButton.onClick.AddListener(OnPlayButtonClick);
        _exitButton.onClick.AddListener(OnExitButtonClick);

        _buttonManager.Init();
        _buttonManager.OnClickButton += OnClickButtonMenu;
    }

    private async void OnPlayButtonClick()
    {
           await _loadingProvider.LoadAndDestroy(new GameplayLoadingOperation(_assetProvider));

      /*   try
        {
            ///Проверка выбрал ли уровень и корабль
         
        }
        catch (Exception e)
        {
            Debug.LogError($"{nameof(MenuWindow)} {nameof(OnPlayButtonClick)} exception: {e.Message}");
        } */
    }

    private async void OnClickButtonMenu(MainMenu mainMenu)
    {

        Hide();

        await mainMenu.Show();

        Show();
      /*   try
        {

        }
        catch (Exception e)
        {
            Debug.LogError($"{nameof(MenuWindow)} {nameof(OnPlayButtonClick)} exception: {e.Message}");
        } */
    }

    private async void OnExitButtonClick()
    {
        var alertPopup = await PopupAlert.Load();
        bool isConfirmed = await alertPopup.Value.AwaitForDecision("Выйти из игры?");

        if (isConfirmed)
            Application.Quit();

        alertPopup.Dispose();
    }

    private void Show() => _canvas.enabled = true;

    private void Hide() => _canvas.enabled = false;

    private void OnDestroy()
    {
        _playButton.onClick.RemoveListener(OnPlayButtonClick);
        _exitButton.onClick.RemoveListener(OnExitButtonClick);

        _buttonManager.OnClickButton -= OnClickButtonMenu;
    }
}
