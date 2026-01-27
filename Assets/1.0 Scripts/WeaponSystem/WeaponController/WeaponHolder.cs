using UnityEngine;
using Zenject;

public class WeaponHolder : MonoBehaviour
{
    [SerializeField] private Weapon[] weapons;

    private Weapon currentWeapon;
    public Weapon CurrentWeapon => currentWeapon;

    private void Awake()
    {
        // 🔥 BẮT BUỘC: disable toàn bộ weapon trước
        foreach (var w in weapons)
        {
            if (w != null)
                w.gameObject.SetActive(false);
        }
    }

    private void Start()
    {
        int equippedIndex = WeaponState.GetEquipped();
        Debug.Log("[WeaponHolder] Equip index = " + equippedIndex);

        EquipByIndex(equippedIndex);
    }

    public void EquipByIndex(int index)
    {
        if (index < 0 || index >= weapons.Length)
        {
            Debug.LogError("Invalid weapon index: " + index);
            return;
        }

        if (currentWeapon != null)
            currentWeapon.gameObject.SetActive(false);

        currentWeapon = weapons[index];

        if (currentWeapon != null)
            currentWeapon.gameObject.SetActive(true);
    }
}
