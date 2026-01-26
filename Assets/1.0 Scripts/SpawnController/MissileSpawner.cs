using System.Collections;
using UnityEngine;

public class MissileSpawner : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject[] obstaclePrefabs;
    [SerializeField] private GameObject alertPrefab;

    [Header("Lane Spawn Points")]
    [SerializeField] private Transform[] obstacleSpawnPoints; // trên trời
    [SerializeField] private Transform[] alertSpawnPoints;    // mặt đất

    [Header("Timing")]
    [SerializeField] private float spawnInterval = 3f;
    [SerializeField] private float startDelay = 1f;
    [SerializeField] private float warningTime = 1.5f;

    // 🧠 MA TRẬN SPAWN (1 = spawn, 0 = trống)
    private int[][] spawnPatterns =
    {
        new int[] {1, 0, 0},
        new int[] {0, 1, 0},
        new int[] {0, 0, 1},
        new int[] {1, 1, 0},
        new int[] {1, 0, 1},
        new int[] {0, 1, 1},
    };

    private void OnEnable()
    {
        InvokeRepeating(nameof(SpawnPattern), startDelay, spawnInterval);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    private void SpawnPattern()
    {
        if (alertPrefab == null || obstaclePrefabs.Length == 0) return;
        if (alertSpawnPoints.Length != obstacleSpawnPoints.Length) return;

        int[] pattern = spawnPatterns[Random.Range(0, spawnPatterns.Length)];

        for (int lane = 0; lane < pattern.Length; lane++)
        {
            if (pattern[lane] == 1)
            {
                StartCoroutine(SpawnLane(lane));
            }
        }
    }

    private IEnumerator SpawnLane(int laneIndex)
    {
        GameObject alert =
            Instantiate(alertPrefab, alertSpawnPoints[laneIndex].position, Quaternion.identity);

        yield return new WaitForSeconds(warningTime);

        int index = Random.Range(0, obstaclePrefabs.Length);
        Instantiate(
            obstaclePrefabs[index],
            obstacleSpawnPoints[laneIndex].position,
            obstacleSpawnPoints[laneIndex].rotation
        );

        Destroy(alert);
    }
}
