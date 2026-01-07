using UnityEngine;

[System.Serializable]
public class PhaseConfig
{
    public PhaseType phaseType;
    public float duration;

    public MonoBehaviour[] enableSpawners;
    public MonoBehaviour[] disableSpawners;
}
