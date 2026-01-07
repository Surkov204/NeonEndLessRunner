using System.Collections;
using UnityEngine;

public enum PhaseType
{
    SpeedRifle,
    Wall,
    Rocket,
    Squads,
    BossFighting 
}

public class SpawnManager : MonoBehaviour
{
    [Header("Phases")]
    [SerializeField] private PhaseConfig[] phases;

    [Header("UI")]
    [SerializeField] private QuestTitleUI questTitleUI;

    private bool forceEndPhase;
    private int phaseIndex;

    private void Start()
    {
        StartCoroutine(PhaseLoop());
    }

    public void NotifyPhaseCompleted()
    {
        forceEndPhase = true;
    }

    private IEnumerator PhaseLoop()
    {
        while (true) // ENDLESS
        {
            PhaseConfig phase = phases[phaseIndex];
            yield return RunPhase(phase);

            phaseIndex++;

            // 👉 nếu KHÔNG có phase tiếp theo (ví dụ Boss chưa tồn tại)
            if (phaseIndex >= phases.Length)
            {
                phaseIndex = 0; // quay lại phase 1
                Debug.Log("===== LOOP RESTART =====");
            }
        }
    }

    private IEnumerator RunPhase(PhaseConfig phase)
    {
        forceEndPhase = false;
        Debug.Log($"{phase.phaseType} Phase ON");

        questTitleUI?.ShowQuestTitle($"{phase.phaseType} Phase");
        yield return new WaitForSeconds(0.5f);

        SetSpawnersActive(phase.disableSpawners, false);
        SetSpawnersActive(phase.enableSpawners, true);

        float timer = 0f;

        while (timer < phase.duration && !forceEndPhase)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        SetSpawnersActive(phase.enableSpawners, false);

        if (phase.phaseType == PhaseType.Squads)
        {
            var squad = FindObjectOfType<SquadPhaseController>();

            if (!forceEndPhase)
            {
                squad?.RetreatAll();
            }
            else
            {
                squad?.ResetPhase();
            }
        }

        questTitleUI?.HideQuestTitle();
    }

    private void SetSpawnersActive(MonoBehaviour[] spawners, bool active)
    {
        if (spawners == null) return;

        foreach (var spawner in spawners)
            if (spawner != null)
                spawner.enabled = active;
    }
}
