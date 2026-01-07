using UnityEngine;

public class GunController : MonoBehaviour
{
    [Header("Gun Setup")]
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float fireRate = 0.2f;
    [SerializeField] private float bulletSpeed = 25f;

    private float lastFireTime;

    public void Fire()
    {
        if (Time.time - lastFireTime < fireRate) return;
        lastFireTime = Time.time;

        GameObject bullet = Instantiate(
            bulletPrefab,
            firePoint.position,
            firePoint.rotation
        );

        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = firePoint.forward * bulletSpeed;
        }
    }
}
