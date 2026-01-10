using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private int damage = 2;

    private void OnTriggerEnter(Collider other)
    {
        Health health = other.transform.root.GetComponent<Health>();
        if (health != null)
        {
            health.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
