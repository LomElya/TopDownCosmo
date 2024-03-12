using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "ShipContent", menuName = "Shop/ShipContent")]
public class ShipContent : Content<ShopShip>, IContents
{
    [field: SerializeField] public ShipType Type { get; private set; }

    private void OnValidate()
    {
        foreach (ShopShip shop in _contents)
        {
            if (shop == null)
            {
                Debug.LogErrorFormat($"Элемент ({shop.ID + 1}) в ({this}) пустой");
                return;
            }
        }
    }

    public ShopShip GetShipConfig(int id)
    {
        ShopShip shopShip = _contents.FirstOrDefault(x => x.ID == id);

        return shopShip;
    }

    public bool IsFoundShipConfig(int id)
    {
        ShopShip shopShip = _contents.FirstOrDefault(x => x.ID == id);

        if (shopShip == null)
            return false;

        return true;
    }
}
