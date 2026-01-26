using UnityEngine;

public class ExplosionForce : MonoBehaviour
{
    [Header("Explosion Settings")]
    public float radius = 5f;
    public float force = 800f;
    public float upwardsModifier = 0.5f;
    public string targetTag = "Enemy";

    [Header("Explosion FX")]
    public GameObject explosionVFX;

    [Header("Timing")]
    public bool explodeOnStart = false;
    public float delay = 0f;

    void Start()
    {
        if (explodeOnStart)
            ExplodeWithDelay();
    }

    public void ExplodeWithDelay()
    {
        Invoke(nameof(Explode), delay);
    }

    public void Explode()
    {
        // spawn hiệu ứng
        if (explosionVFX != null)
            Instantiate(explosionVFX, transform.position, Quaternion.identity);

        // tìm các vật thể trong vùng nổ
        Collider[] cols = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider col in cols)
        {
            if (col.CompareTag(targetTag))
            {
                Rigidbody rb = col.attachedRigidbody;
                if (rb != null)
                {
                    rb.AddExplosionForce(force, transform.position, radius, upwardsModifier, ForceMode.Impulse);
                }
            }
        }

    }

    // vẽ radius để dễ chỉnh trong editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
