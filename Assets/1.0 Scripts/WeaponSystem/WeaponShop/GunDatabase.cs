using System.Collections.Generic;
using UnityEngine;

public class GunDatabase : MonoBehaviour
{
    [SerializeField] private List<GunConfig> guns;

    public GunConfig GetGun(int index)
        => guns.Find(g => g.gunIndex == index);

    public int GetPrice(int index)
        => GetGun(index)?.price ?? int.MaxValue;

    public bool IsShopItem(int index)
        => GetGun(index)?.isShopItem ?? false;
}
