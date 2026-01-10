using UnityEngine;
using TMPro;
using Zenject;

public class CoinUIText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinText;

    private CurrencyService currency;
    private int runCoin; // 👉 chỉ dùng cho UI trong màn

    [Inject]
    void Construct(ICurrencyService currencyService)
    {
        currency = currencyService as CurrencyService;
    }

    private void Start()
    {
        runCoin = 0;
        coinText.text = "0";
        currency.OnChanged += OnTotalChanged;
    }

    private void OnDestroy()
    {
        currency.OnChanged -= OnTotalChanged;
    }

    private void OnTotalChanged(int totalValue)
    {
        runCoin++;
        coinText.text = runCoin.ToString();
    }
}