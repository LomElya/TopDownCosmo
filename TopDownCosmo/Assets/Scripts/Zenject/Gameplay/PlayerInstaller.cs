using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    [Header("Ship")]
    [SerializeField] private ShipFactory _shipFactory;
    [SerializeField] private ShipSpawner _shipSpawner;

    [Header("Health Bar")]
    [SerializeField] private HealthBar _healthBar;

    public override void InstallBindings()
    {
        BindShip();
    }

    private void BindShip()
    {
        Container.Bind<ShipFactory>().FromInstance(_shipFactory).AsSingle();
        Container.BindInterfacesAndSelfTo<ShipSpawner>().FromInstance(_shipSpawner).AsSingle().NonLazy();

        Container.BindInterfacesAndSelfTo<HealthBar>().FromInstance(_healthBar).AsSingle();
    }
}
