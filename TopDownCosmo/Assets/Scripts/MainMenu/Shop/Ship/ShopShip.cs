using UnityEngine;

[CreateAssetMenu(fileName = "ShopShip", menuName = "Shop/ShopShip")]
public class ShopShip : Shop
{
  [field: SerializeField] public Sprite Image { get; private set; }
  [field: SerializeField, Range(1, 10)] public float MaxHealth { get; private set; }
  [field: SerializeField, Range(1, 10)] public float Speed { get; private set; }

  public ShipStats ToStats() => new ShipStats(MaxHealth, Speed);
}
