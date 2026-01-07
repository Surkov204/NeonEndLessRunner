using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 6f;
    [SerializeField] private Vector3 moveDirection = new Vector3(-1, 0, 0);
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private int damageToPlayer = 10;
    private Health health;

    private void Awake()
    {
        health = GetComponent<Health>();
        if (health != null)
        {
            health.OnDeath += OnDead;
        }
    }

    private void Update()
    {
        transform.position += moveDirection.normalized * moveSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Health playerHealth = other.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageToPlayer);
            }

            OnDead();
        }
    }

    private void OnDead()
    {
        if (explosionPrefab != null)
        {
            GameObject explosion =
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);

            Destroy(explosion, 0.25f);
        }

        Destroy(gameObject);
    }   
}
