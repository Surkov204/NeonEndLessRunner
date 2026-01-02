using UnityEngine;
using UnityEngine.Animations.Rigging;

public class PlayerShooter : MonoBehaviour
{
    [Header("Rig")]
    public Rig weaponRig;
    public float rigBlendSpeed = 10f;

    [Header("Weapon")]
    public Transform muzzle;
    public GameObject bulletPrefab;
    public float bulletSpeed = 40f;
    public float fireRate = 0.12f;

    float targetRigWeight = 0f;
    float fireTimer = 0f;
    bool isFiring = false;

    void Update()
    {
        HandleInput();
        UpdateRig();
        HandleFire();
    }

    void HandleInput()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        isFiring = Input.GetMouseButton(0);
#else
        isFiring = Input.touchCount > 0;
#endif
        targetRigWeight = isFiring ? 1f : 0f;
    }

    void UpdateRig()
    {
        weaponRig.weight = Mathf.Lerp(
            weaponRig.weight,
            targetRigWeight,
            Time.deltaTime * rigBlendSpeed
        );
    }

    void HandleFire()
    {
        if (!isFiring) return;

        fireTimer -= Time.deltaTime;
        if (fireTimer <= 0f)
        {
            Fire();
            fireTimer = fireRate;
        }
    }

    void Fire()
    {
        if (bulletPrefab == null || muzzle == null) return;

        GameObject bullet = Instantiate(bulletPrefab, muzzle.position, muzzle.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
            rb.velocity = muzzle.forward * bulletSpeed;

        Destroy(bullet, 3f);
    }
}
