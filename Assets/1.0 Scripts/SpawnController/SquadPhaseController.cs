using UnityEngine;
using System.Collections.Generic;

public class SquadPhaseController : MonoBehaviour
{
    [SerializeField] private int requiredKillCount = 2;
    [SerializeField] private SpawnManager spawnManager;

    private int currentKill;
    private List<NPCVehicle> activeVehicles = new();

    public void RegisterVehicle(NPCVehicle vehicle)
    {
        if (vehicle == null) return;

        activeVehicles.Add(vehicle);

        Health health = vehicle.GetComponent<Health>();
        if (health != null)
        {
            health.OnDeath -= OnUnitDead;
            health.OnDeath += OnUnitDead;
        }
    }

    private void OnUnitDead()
    {
        currentKill++;
        Debug.Log($"[Squad] {currentKill}/{requiredKillCount}");

        if (currentKill >= requiredKillCount)
        {
            spawnManager.NotifyPhaseCompleted();
        }
    }

    public void RetreatAll()
    {
        foreach (var vehicle in activeVehicles)
        {
            if (vehicle != null)
                vehicle.Retreat();
        }

        activeVehicles.Clear();
        ResetPhase();
    }

    public void ResetPhase()
    {
        currentKill = 0;
    }
}
