using UnityEngine;
using TMPro;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance;

    [SerializeField] private TextMeshProUGUI coinText;
    private int coinCount;

    private void Awake()
    {
        Instance = this;
        UpdateText();
    }

    public void AddCoin(int amount)
    {
        coinCount += amount;
        UpdateText();
    }

    private void UpdateText()
    {
        coinText.text = coinCount.ToString();
    }
}
