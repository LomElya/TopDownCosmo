using UnityEngine;
using Zenject;

public class GlobalInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        LoadingOperations();

        BindSaveData();

        BindInput();

        BindMainService();
    }

    private void BindSaveData()
    {
        BindUserStateCommunicator();
        Container.Bind<UserContainer>().AsSingle();
    }

    private void BindInput()
    {
        if (SystemInfo.deviceType == DeviceType.Desktop) // Если ПК ...
            Container.BindInterfacesAndSelfTo<DesktopInput>().AsSingle();
        else
            Container.BindInterfacesAndSelfTo<DesktopInput>().AsSingle();
    }

    private void BindMainService()
    {
        Container.BindInterfacesAndSelfTo<Timer>().FromInstance(new Timer(this)).AsTransient();
        Container.BindInterfacesAndSelfTo<Level>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<Wallet>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<Score>().AsSingle().NonLazy();

        Container.BindInterfacesAndSelfTo<PauseManager>().AsSingle().NonLazy();
    }

    private void LoadingOperations()
    {
        Container.BindInterfacesAndSelfTo<AssetProvider>().AsSingle();
        Container.BindInterfacesAndSelfTo<LoadingScreenProvider>().AsSingle();
    }

    private void BindUserStateCommunicator()
    {
        Container.BindInterfacesAndSelfTo<LocalStateCommunicator>().AsSingle();
    }
}


