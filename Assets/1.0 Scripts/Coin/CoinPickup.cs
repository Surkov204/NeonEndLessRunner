using UnityEngine;
using Zenject;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] private int coinValue = 1;

    private CoinFlyUI coinFlyUI;

    [Inject]
    void Construct(CoinFlyUI flyUI)
    {
        Debug.Log($"CoinPickup injected. flyUI null? {flyUI == null}");
        coinFlyUI = flyUI;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        if (coinFlyUI == null)
        {
            Debug.LogError("CoinFlyUI is NULL in CoinPickup. Injection failed.");
            return;
        }


        coinFlyUI.SpawnCoinFly(transform.position, coinValue);  
        Destroy(gameObject);
    }
}
