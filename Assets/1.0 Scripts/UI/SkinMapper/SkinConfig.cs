using UnityEngine;

[CreateAssetMenu(menuName = "Shop/Skin Config")]
public class SkinConfig : ScriptableObject
{
    [Header("Shop (Optional)")]
    public bool isShopItem = false;
    [Header("Identity")]
    public int skinIndex;

    [Header("Shop")]
    public int price;

    [Header("health")]
    public int health;

    [Header("Auto Regen Value")]
    public int healthRegen;

    [Header("Speed Value")]
    public int speedMove;
}
