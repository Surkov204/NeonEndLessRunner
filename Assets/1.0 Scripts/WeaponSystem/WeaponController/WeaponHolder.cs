using UnityEngine;
using Zenject;

public class WeaponHolder : MonoBehaviour
{
    [SerializeField] private Weapon[] weapons;

    [Header("TEST ONLY")]
    [SerializeField] private bool useTestWeapon = false;
    [SerializeField] private Weapon testWeapon;

    private Weapon currentWeapon;
    public Weapon CurrentWeapon => currentWeapon;

    private void Awake()
    {
        foreach (var w in weapons)
            w.gameObject.SetActive(false);

        if (useTestWeapon && testWeapon != null)
        {
            EquipWeapon(testWeapon);
        }
    }

    public void EquipWeapon(Weapon weapon)
    {
        if (currentWeapon == weapon) return;

        if (currentWeapon != null)
            currentWeapon.gameObject.SetActive(false);

        currentWeapon = weapon;

        if(currentWeapon != null)
        {
            currentWeapon.gameObject.SetActive(true);
        }
    }

    public void EquipByIndex(int index)
    {
        if (index < 0 || index >= weapons.Length) return;
        EquipWeapon(weapons[index]);
    }
}
