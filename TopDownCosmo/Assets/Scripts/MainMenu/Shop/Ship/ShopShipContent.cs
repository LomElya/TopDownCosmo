using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopShipContent", menuName = "Shop/Content/ShopShipContent")]
public class ShopShipContent : ShopContent<ShipContent>
{
    private void OnValidate()
    {
        int id = 0;

        foreach (ShipContent shopContent in _contents)
        {
            foreach (ShopShip content in shopContent.Contents)
            {
                content.SetID(id);
                id++;
            }
        }
    }

    public ShipContent GetShipContents(ShipType type)
    {
        ShipContent shipContent = _contents.FirstOrDefault(x => x.Type == type);

        if (shipContent == null)
        {
            Debug.LogErrorFormat($"Контент с типом ({type}) не найден");
            return null;
        }

        return shipContent;
    }

    public ShopShip GetShipConfig(int id)
    {
        ShopShip shopShip;

        foreach (ShipContent shipContent in _contents)
        {
            if (shipContent.IsFoundShipConfig(id))
            {
                shopShip = shipContent.GetShipConfig(id);
                return shopShip;
            }
        }

        Debug.LogErrorFormat($"Корабль с ID ({id}) не найден");
        return null;
    }

}
