using UnityEngine;
using System.Collections;
using Zenject;
using static SoundFXLibrary;

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
    public float ReloadTime => data.ReloadTime;
    public bool IsReloading => isReloading;

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

        GameObject projectile = MultiPrefabPool.Instance.Spawn(
            data.BulletPrefab,
            muzzle.position,
            muzzle.rotation
        );
        AudioManager.Instance.PlaySoundFX(SoundFXName.Shot);
        Rigidbody[] rbs = projectile.GetComponentsInChildren<Rigidbody>();

        foreach (var rb in rbs)
        {
            if (rb == null) continue;

            rb.velocity = rb.transform.forward * data.BulletSpeed;
        }
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
