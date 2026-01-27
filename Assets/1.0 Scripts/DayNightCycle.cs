using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    [System.Serializable]
    public class DayPhase
    {
        public Color lightColor;
        public float intensity;
        public Vector3 rotation;
        public float duration; 
    }

    [Header("Light Reference")]
    [SerializeField] private Light directionalLight;

    [Header("Day Night Phases (1 → 2 → 3 → 4 → loop)")]
    [SerializeField] private DayPhase[] phases;

    private int currentPhaseIndex = 0;
    private float timer = 0f;

    private void Start()
    {
        ApplyPhase(phases[0]);
    }

    private void Update()
    {
        if (phases.Length < 2) return;

        DayPhase current = phases[currentPhaseIndex];
        DayPhase next = phases[(currentPhaseIndex + 1) % phases.Length];

        timer += Time.deltaTime;
        float t = Mathf.Clamp01(timer / current.duration);

        directionalLight.color = Color.Lerp(
            current.lightColor,
            next.lightColor,
            t
        );

        directionalLight.intensity = Mathf.Lerp(
            current.intensity,
            next.intensity,
            t
        );

        Vector3 rot = Vector3.Lerp(
            current.rotation,
            next.rotation,
            t
        );
        transform.rotation = Quaternion.Euler(rot);

        if (t >= 1f)
        {
            timer = 0f;
            currentPhaseIndex = (currentPhaseIndex + 1) % phases.Length;
        }
    }

    private void ApplyPhase(DayPhase phase)
    {
        directionalLight.color = phase.lightColor;
        directionalLight.intensity = phase.intensity;
        transform.rotation = Quaternion.Euler(phase.rotation);
    }
}
