using UnityEngine;
using System.Collections;
using Zenject;

public class Weapon : MonoBehaviour
{
    [SerializeField] private WeaponData data;
    [SerializeField] private Transform muzzle;

    private int currentAmmo;
    private float fireTimer;
    private bool isReloading;

    private SignalBus signalBus;

    public int CurrentAmmo => currentAmmo;
    public int MaxAmmo => data.MagazineSize;

    [Inject]
    public void Construct(SignalBus signalBus)
    {
        this.signalBus = signalBus;
    }

    private void OnEnable()
    {
        currentAmmo = data.MagazineSize;
        StartCoroutine(DeferredSync());
    }

    private IEnumerator DeferredSync()
    {
        yield return null; 
        FireAmmoSignal();
    }

    public void TryFire()
    {
        if (isReloading) return;
        if (currentAmmo <= 0) return;

        fireTimer -= Time.deltaTime;
        if (fireTimer > 0f) return;

        Fire();
        fireTimer = data.FireRate;
    }

    private void Fire()
    {
        currentAmmo--;
        FireAmmoSignal();

        GameObject bullet = Instantiate(data.BulletPrefab, muzzle.position, muzzle.rotation);
        if (bullet.TryGetComponent(out Rigidbody rb))
            rb.velocity = muzzle.forward * data.BulletSpeed;

        Destroy(bullet, 3f);
    }

    public void Reload()
    {
        if (isReloading) return;
        if (currentAmmo == data.MagazineSize) return;
        StartCoroutine(ReloadRoutine());
    }

    private IEnumerator ReloadRoutine()
    {
        isReloading = true;
        yield return new WaitForSeconds(data.ReloadTime);
        currentAmmo = data.MagazineSize;
        isReloading = false;
        FireAmmoSignal();
    }

    private void FireAmmoSignal()
    {
        if (signalBus == null) return; 
        signalBus.Fire(new AmmoChangedSignal { Current = currentAmmo, Max = data.MagazineSize });
    }
}
