using UnityEngine;

[CreateAssetMenu(menuName = "Shop/Gun Config")]
public class GunConfig : ScriptableObject
{
    [Header("Identity")]
    public int gunIndex;

    [Header("Shop")]
    public bool isShopItem = true;
    public int price;

    [Header("Visual (Optional)")]
    public Material gunMaterial; // OPTION – có thì dùng, không thì thôi
}