using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject[] coinPrefabs;
    [SerializeField] private GameObject[] obstaclePrefabs;

    [Header("Spawn Point")]
    [SerializeField] private Transform spawnPoint;

    [Header("Timing")]
    [SerializeField] private float spawnInterval = 2f;
    [SerializeField] private float startDelay = 1f;

    private enum SpawnState { Coin, Obstacle }
    private SpawnState state = SpawnState.Coin;

    private void OnEnable()
    {
        InvokeRepeating(nameof(Spawn), startDelay, spawnInterval);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    private void Spawn()
    {
        if (spawnPoint == null) return;

        switch (state)
        {
            case SpawnState.Coin:
                SpawnCoin();
                state = SpawnState.Obstacle; // SAU COIN LÀ OBSTACLE
                break;

            case SpawnState.Obstacle:
                SpawnObstacle();
                state = SpawnState.Coin;     // SAU OBSTACLE LÀ COIN
                break;
        }
    }

    private void SpawnCoin()
    {
        if (coinPrefabs.Length == 0) return;

        int index = Random.Range(0, coinPrefabs.Length);
        Instantiate(coinPrefabs[index], spawnPoint.position, spawnPoint.rotation);
    }

    private void SpawnObstacle()
    {
        if (obstaclePrefabs.Length == 0) return;

        int index = Random.Range(0, obstaclePrefabs.Length);
        Instantiate(obstaclePrefabs[index], spawnPoint.position, spawnPoint.rotation);
    }
}
