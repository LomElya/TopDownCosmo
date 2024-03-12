using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "ShipFactory", menuName = "Factory/ShipFactory")]
public class ShipFactory : GameObjectFactory
{
    [SerializeField] private List<ShipConfig> _configs;

    public Ship Get(int id, Transform spawnPoint)
    {
        var parent = spawnPoint.transform.parent;

        ShipConfig config = GetConfig(id);

        Ship spawnedShip = Instantiate(config.Prefab, spawnPoint.position, Quaternion.identity, null);

        spawnedShip.transform.SetParent(parent);

        spawnedShip.Init(config.GetStats());

        return spawnedShip;
    }

    public Ship Get(int id)
    {
        ShipConfig config = GetConfig(id);

        Ship spawnedShip = CreateGameObjectInstance(config.Prefab);

        if (spawnedShip == null)
        {
            Debug.LogErrorFormat($"Префаб корабля ID ({id}) не найден");
            return null;
        }

        spawnedShip.Init(config.GetStats());

        return spawnedShip;
    }

    public ShipConfig GetConfig(int id)
    {
        ShipConfig config = _configs.FirstOrDefault(x => x.ShopShip.ID == id);

        if (config == null)
        {
            Debug.LogErrorFormat($"Корабль с ID ({id}) не найден");
            return null;
        }

        return config;
    }
}
