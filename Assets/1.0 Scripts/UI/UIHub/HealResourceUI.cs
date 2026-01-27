using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealResourceUI : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private HealResource healResource;
    [SerializeField] private Slider greenSlider;
    [SerializeField] private TextMeshProUGUI greenText;

    private void OnEnable()
    {
        if (healResource != null)
            healResource.OnResourceChanged += UpdateUI;
    }

    private void OnDisable()
    {
        if (healResource != null)
            healResource.OnResourceChanged -= UpdateUI;
    }

    private void Start()
    {
        if (healResource != null)
            UpdateUI(healResource.Current, healResource.Max);
    }

    private void UpdateUI(int current, int max)
    {
        greenSlider.maxValue = max;
        greenSlider.value = current;

        if (greenText != null)
            greenText.text = $"{current} / {max}";
    }
}
