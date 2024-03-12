using Zenject;
using UnityEngine;
using System.Collections.Generic;

public class LoadersInstaller : MonoInstaller
{
    [SerializeField] private LevelConfig _levelConfigs;
    [SerializeField] private ShopShipContent _shipContent;

    public override void InstallBindings()
    {
        Debug.Log("2");
        BindLoaders();
    }

    private void BindLoaders()
    {
        Container.BindInterfacesAndSelfTo<LevelConfig>().FromInstance(_levelConfigs).AsSingle();
        Container.BindInterfacesAndSelfTo<ShopShipContent>().FromInstance(_shipContent).AsSingle();

        /*   _levelLoader = _levelConfigs;
          _shipLoader = _shipContent;

          List<ILoader> loaders = new List<ILoader>();
          loaders.Add(_levelLoader);
          loaders.Add(_shipLoader);

          Container.Bind<DataLoader>().FromInstance(new DataLoader(loaders)).AsSingle(); */
    }
}
