using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] private int coinValue = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        CoinFlyUI.Instance.SpawnCoinFly(transform.position, coinValue);

        Destroy(gameObject);
    }
}
