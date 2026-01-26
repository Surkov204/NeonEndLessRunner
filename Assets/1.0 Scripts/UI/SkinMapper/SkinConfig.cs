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

    [Header("Stats")]
    public int health;
}
