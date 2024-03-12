using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class InteractableInstaller : MonoInstaller
{
    [Header("Interactable")]
    [SerializeField] private InteractableFactory _interactableFactory;
    [SerializeField] private InteractableSpawner _interactableSpawner;
    [SerializeField] private InteractableMover _interactableMover;


    public override void InstallBindings()
    {
        BindInteractableSpawner();
    }

    private void BindInteractableSpawner()
    {
        Container.Bind<InteractableFactory>().FromInstance(_interactableFactory).AsSingle();
        Container.Bind<InteractableMover>().FromInstance(_interactableMover).AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<InteractableSpawner>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<InteractableController>().AsSingle().NonLazy();
    }
}
