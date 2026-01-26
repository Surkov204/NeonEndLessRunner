using System.Collections.Generic;
using UnityEngine;

public class SkinDatabase : MonoBehaviour
{
    [SerializeField] private List<SkinConfig> skins;

    public SkinConfig GetSkin(int index)
    {
        return skins.Find(s => s.skinIndex == index);
    }

    public int GetHealth(int index)
    {
        var skin = GetSkin(index);
        return skin != null ? skin.health : 0;
    }

    public bool IsShopItem(int index)
    {
        var skin = GetSkin(index);
        return skin != null && skin.isShopItem;
    }

    public int GetPrice(int index)
    {
        var skin = GetSkin(index);
        if (skin == null || !skin.isShopItem)
            return 0;

        return skin.price;
    }
}
