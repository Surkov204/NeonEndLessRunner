using UnityEngine;

public class NPCShooter : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GunController gun;

    [Header("Auto Fire")]
    [SerializeField] private bool autoFire = true;

    [Tooltip("Min - Max fire interval")]
    [SerializeField] private Vector2 fireIntervalRange = new Vector2(1.1f, 2.2f);

    [Header("Optional Target")]
    [SerializeField] private Transform target;
    [SerializeField] private float shootRange = 15f;

    private float fireInterval;
    private float timer;

    private void Start()
    {
        // 🎲 mỗi NPC có nhịp bắn riêng
        fireInterval = Random.Range(
            fireIntervalRange.x,
            fireIntervalRange.y
        );

        // 🎲 lệch pha ban đầu
        timer = Random.Range(0f, fireInterval);
    }

    private void Update()
    {
        if (!autoFire || gun == null) return;

        if (target != null)
        {
            float dist = Vector3.Distance(transform.position, target.position);
            if (dist > shootRange) return;

            Vector3 lookDir = target.position - transform.position;
            lookDir.y = 0;
            transform.forward = lookDir.normalized;
        }

        timer += Time.deltaTime;
        if (timer >= fireInterval)
        {
            timer = 0f;
            gun.Fire();

            // 🔥 mỗi lần bắn có thể đổi nhịp nhẹ (tùy thích)
            fireInterval = Random.Range(
                fireIntervalRange.x,
                fireIntervalRange.y
            );
        }
    }
}
