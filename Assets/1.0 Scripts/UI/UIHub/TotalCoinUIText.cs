using UnityEngine;
using TMPro;
using Zenject;

public class TotalCoinUIText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinText;

    private CurrencyService currency;

    [Inject]
    void Construct(ICurrencyService currencyService)
    {
        currency = currencyService as CurrencyService;
    }

    private void Start()
    {
        coinText.text = currency.Coins.ToString();
        currency.OnChanged += UpdateText;
    }

    private void OnDestroy()
    {
        currency.OnChanged -= UpdateText;
    }

    private void UpdateText(int totalValue)
    {
        coinText.text = totalValue.ToString();
    }
}
