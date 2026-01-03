using UnityEngine;
using UnityEngine.Animations.Rigging;

public class PlayerShooter : MonoBehaviour
{
    [Header("Rig")]
    [SerializeField] private Rig weaponRig;
    [SerializeField] private float rigBlendSpeed = 10f;

    [Header("Weapon")]
    [SerializeField] private Transform muzzle;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed = 40f;
    [SerializeField] private float fireRate = 0.12f;

    private float targetRigWeight = 0f;
    private float fireTimer = 0f;
    private bool isFiring = false;

    private void Update()
    {
        HandleInput();
        UpdateRig();
        HandleFire();
    }

    private void HandleInput()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        isFiring = Input.GetMouseButton(0);
#else
        isFiring = Input.touchCount > 0;
#endif
        targetRigWeight = isFiring ? 1f : 0f;
    }

    private void UpdateRig()
    {
        weaponRig.weight = Mathf.Lerp(
            weaponRig.weight,
            targetRigWeight,
            Time.deltaTime * rigBlendSpeed
        );
    }

    private void HandleFire()
    {
        if (!isFiring) return;

        fireTimer -= Time.deltaTime;
        if (fireTimer <= 0f)
        {
            Fire();
            fireTimer = fireRate;
        }
    }

    private void Fire()
    {
        if (bulletPrefab == null || muzzle == null) return;

        GameObject bullet = Instantiate(bulletPrefab, muzzle.position, muzzle.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
            rb.velocity = muzzle.forward * bulletSpeed;

        Destroy(bullet, 3f);
    }
}
