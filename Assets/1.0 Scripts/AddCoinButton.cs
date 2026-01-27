using UnityEngine;
using Zenject;

public class AddCoinButton : MonoBehaviour
{
    private ICurrencyService currencyService;

    [Inject]
    public void Construct(ICurrencyService currencyService)
    {
        this.currencyService = currencyService;
    }

    public void Add1000Coins()
    {
        currencyService.Add(1000);
        Debug.Log("Added 1000 coins (DEBUG)");
    }
}
