using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnermyBullet : MonoBehaviour
{
    [SerializeField] private float lifeTime = 3f;
    [SerializeField] private int damage = 1;
    [SerializeField] private GameObject explosionPrefab;

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Health playerHealth = other.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
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

            Destroy(explosion, 0.4f);
        }

        Destroy(gameObject);
    }
}
