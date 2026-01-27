using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private int damage = 2;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Bullet hit: {other.name}, tag={other.tag}, layer={other.gameObject.layer}");

        if (!other.CompareTag("Obstacle"))
            return;

        var health = other.GetComponentInParent<Health>();
        if (health == null)
        {
            Debug.LogWarning($"No Health on {other.name}");
            return;
        }

        Debug.Log($"Applying damage to {other.name}");
        health.TakeDamage(damage);
        AudioManager.Instance.PlaySoundFX(SoundFXLibrary.SoundFXName.Missile);
        MultiPrefabPool.Instance.Despawn(gameObject);
    }
}
