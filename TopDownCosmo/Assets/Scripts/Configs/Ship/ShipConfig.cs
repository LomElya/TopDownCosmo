using UnityEngine;

[System.Serializable]
public class ShipConfig
{
    [field: SerializeField] public Ship Prefab { get; private set; }
    [field: SerializeField] public ShopShip ShopShip { get; private set; }

    public ShipStats GetStats() => ShopShip.ToStats();
}
