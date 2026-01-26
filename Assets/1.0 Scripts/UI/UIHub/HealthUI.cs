using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private Health playerHealth;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private TextMeshProUGUI healthText; 

    private void OnEnable()
    {
        if (playerHealth != null)
            playerHealth.OnHealthChanged += UpdateUI;
    }

    private void OnDisable()
    {
        if (playerHealth != null)
            playerHealth.OnHealthChanged -= UpdateUI;
    }

    private void Start()
    {
        if (playerHealth != null)
            UpdateUI(playerHealth.CurrentHealth, playerHealth.MaxHealth);
    }

    private void UpdateUI(int current, int max)
    {
        healthSlider.maxValue = max;
        healthSlider.value = current;

        if (healthText != null)
            healthText.text = $"{current} / {max}";
    }
}
