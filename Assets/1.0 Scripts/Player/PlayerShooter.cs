using UnityEngine;
using UnityEngine.Animations.Rigging;

public class PlayerShooter : MonoBehaviour
{
    [Header("Rig")]
    [SerializeField] private Rig weaponRig;
    [SerializeField] private float rigBlendSpeed = 10f;

    [Header("Refs")]
    [SerializeField] private WeaponHolder weaponHolder;

    private float targetRigWeight;
    private bool isFiring;

    private void Update()
    {
        UpdateRig();

        if (!isFiring) return;

        Weapon weapon = weaponHolder.CurrentWeapon;
        if (weapon != null)
            weapon.TryFire();
    }

    public void StartFire()
    {
        isFiring = true;
        targetRigWeight = 1f;
    }

    public void StopFire()
    {
        isFiring = false;
        targetRigWeight = 0f;
    }

    public void OnReloadButton()
    {
        weaponHolder.CurrentWeapon?.Reload();
    }

    private void UpdateRig()
    {
        if (!weaponRig) return;

        weaponRig.weight = Mathf.Lerp(
            weaponRig.weight,
            targetRigWeight,
            Time.deltaTime * rigBlendSpeed
        );
    }
}
