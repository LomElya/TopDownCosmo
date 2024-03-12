using UnityEngine;
using Zenject;

public class ServiceInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        BindProcessor();
        BindMovementService();

        var gameStateFabric = new GameStateFabric();
        Container.BindInstance(gameStateFabric);
        Container.QueueForInject(gameStateFabric);

        var turnManager = new TurnManager(this);
        Container.Bind<TurnManager>().FromInstance(turnManager).AsSingle();
        Container.QueueForInject(turnManager);
    }

    private void BindMovementService()
    {
        Container.BindInterfacesAndSelfTo<MovementHandler>().AsSingle().NonLazy();
    }

    private void BindProcessor()
    {
        Container.BindInterfacesAndSelfTo<SurviveScenarioProcessor>().AsSingle().NonLazy();
    }
}
