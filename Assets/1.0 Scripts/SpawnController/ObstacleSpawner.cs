using UnityEngine;
using Zenject;

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

        if (state == SpawnState.Coin)
        {
            SpawnFrom(coinPrefabs);
            state = SpawnState.Obstacle;
        }
        else
        {
            SpawnFrom(obstaclePrefabs);
            state = SpawnState.Coin;
        }
    }

    private void SpawnFrom(GameObject[] prefabs)
    {
        if (prefabs == null || prefabs.Length == 0) return;

        int index = Random.Range(0, prefabs.Length);
        MultiPrefabPool.Instance.Spawn(prefabs[index], spawnPoint.position, spawnPoint.rotation);
    }
}
