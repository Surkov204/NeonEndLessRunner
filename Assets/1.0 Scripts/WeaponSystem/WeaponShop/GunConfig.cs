using UnityEngine;

[CreateAssetMenu(menuName = "Shop/Gun Config")]
public class GunConfig : ScriptableObject
{
    [Header("Identity")]
    public int gunIndex;
    public string gunName;

    [Header("Shop")]
    public bool isShopItem = true;
    public int price;

    [Header("Stats")]
    public int maxAmmo;
    public float fireRate;
    public float reloadTime;
    public float muzzleVelocity;


    [Header("Visual (Optional)")]
    public Material gunMaterial; 
}