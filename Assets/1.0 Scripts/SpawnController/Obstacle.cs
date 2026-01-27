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
    }

    private void OnEnable()
    {
        if (health != null)
        {
            health.OnDeath -= OnDead;
            health.OnDeath += OnDead;
            health.ResetHealth();
        }
    }

    private void OnDisable()
    {
        if (health != null)
            health.OnDeath -= OnDead;
    }

    private void Update()
    {
        transform.position += moveDirection.normalized * moveSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var playerHealth = other.GetComponent<Health>();
            if (playerHealth != null)
                playerHealth.TakeDamage(damageToPlayer);
            AudioManager.Instance.PlaySoundFX(SoundFXLibrary.SoundFXName.Crash);
            OnDead();
        }
    }

    private void OnDead()
    {
        if (explosionPrefab != null)
        {
            var explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(explosion, 0.25f);
        }

        MultiPrefabPool.Instance.Despawn(gameObject);
    }
}
