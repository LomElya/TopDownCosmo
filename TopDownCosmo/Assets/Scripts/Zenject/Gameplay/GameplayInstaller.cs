using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameplayInstaller : MonoInstaller
{
    [SerializeField] private Gameplay _gameplay;

    public override void InstallBindings()
    {
        BindSignals();

        /*  var effectApplier = new EffectApplier();
          Container.BindInstance(effectApplier); */

        Container.BindInterfacesAndSelfTo<Gameplay>().FromInstance(_gameplay).AsSingle();


    }

    private void BindSignals()
    {
        SignalBusInstaller.Install(Container);

        Container.DeclareSignal<UnitUsedAbilitySignal>().OptionalSubscriber();
        Container.DeclareSignal<PlayerDeadSignal>().OptionalSubscriber();
        Container.DeclareSignal<PauseGameSignal>().OptionalSubscriber();
    }
}
