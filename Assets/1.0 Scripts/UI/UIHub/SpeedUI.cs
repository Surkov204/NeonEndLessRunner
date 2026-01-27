using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpeedUI : MonoBehaviour
{
    [SerializeField] private PlayerLaneMove laneMove;
    [SerializeField] private Slider speedSlider;
    [SerializeField] private TextMeshProUGUI speedText;

    [Header("Speed Range")]
    [SerializeField] private float maxSpeed = 10f; 

    private void Start()
    {
        UpdateUI();
    }

    private void Update()
    {
        UpdateUI(); 
    }

    private void UpdateUI()
    {
        float speed = laneMove.CurrentSpeed;

        speedSlider.maxValue = 1f;
        speedSlider.value = speed / maxSpeed;

        if (speedText != null)
            speedText.text = speed.ToString("0.0");
    }
}
