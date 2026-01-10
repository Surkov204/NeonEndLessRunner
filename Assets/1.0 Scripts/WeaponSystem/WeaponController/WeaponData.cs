using UnityEngine;

[CreateAssetMenu(menuName = "Weapon/Weapon Data")]
public class WeaponData : ScriptableObject
{
    [SerializeField] private string weaponName;

    [Header("Ammo")]
    [SerializeField] private int magazineSize = 30;
    [SerializeField] private float reloadTime = 1.5f;

    [Header("Fire")]
    [SerializeField] private float fireRate = 0.12f;
    [SerializeField] private float bulletSpeed = 40f;

    [Header("Visual")]
    [SerializeField] private GameObject bulletPrefab;

    public string WeaponName => weaponName;
    public int MagazineSize => magazineSize;
    public float ReloadTime => reloadTime;
    public float FireRate => fireRate;
    public float BulletSpeed => bulletSpeed;
    public GameObject BulletPrefab => bulletPrefab;
}
