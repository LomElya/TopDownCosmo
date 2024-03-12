using UnityEngine;
using Zenject;

public class LevelInstaller : MonoInstaller
{
    [SerializeField] private ProgressLevelView _progressLevel;

    [SerializeField] private LevelConfig _levelConfig;
    [SerializeField] private GUIHolder _guiHolder;
    [SerializeField] private GameResultMenu _gameResultMenu;

    public override void InstallBindings()
    {
        BindConfig();

        BindProgressLevel();

        BindLevelService();

        BindGUIHolder();
    }

    private void BindConfig()
    {
        Container.BindInterfacesAndSelfTo<LevelConfig>().FromInstance(_levelConfig).AsSingle();
    }

    private void BindProgressLevel()
    {
        Container.BindInterfacesAndSelfTo<ProgressLevelView>().FromInstance(_progressLevel).AsSingle();
    }

    private void BindLevelService()
    {
        Container.BindInterfacesAndSelfTo<GameResultMenu>().FromInstance(_gameResultMenu).AsSingle();
    }

    private void BindGUIHolder()
    {
        Container.BindInterfacesAndSelfTo<GUIHolder>().FromInstance(_guiHolder).AsSingle();
    }
}
