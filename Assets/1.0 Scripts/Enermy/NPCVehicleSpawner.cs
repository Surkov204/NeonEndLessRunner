using UnityEngine;

public class NPCVehicleSpawner : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private NPCVehicle vehiclePrefab;

    [Header("Points")]
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform stopPoint;

    [Header("Spawn")]
    [SerializeField] private float spawnDelay = 1f;
    [SerializeField] private SquadPhaseController squadController;

    private void OnEnable()
    {
        Invoke(nameof(SpawnVehicle), spawnDelay);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    private void SpawnVehicle()
    {
        if (vehiclePrefab == null || spawnPoint == null || stopPoint == null)
            return;

        NPCVehicle vehicle = Instantiate(
            vehiclePrefab,
            spawnPoint.position,
            spawnPoint.rotation
        );

        vehicle.InitSpawnPoint(spawnPoint.position);
        vehicle.MoveTo(stopPoint.position);

        Health health = vehicle.GetComponent<Health>();
        if (health != null && squadController != null)
        {
            squadController.RegisterVehicle(vehicle);
        }
    }
}

