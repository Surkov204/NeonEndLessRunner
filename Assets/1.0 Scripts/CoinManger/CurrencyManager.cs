using UnityEngine;
using System;

public class CurrencyService : ICurrencyService
{
    private const string COIN_KEY = "TOTAL_COINS";
    private int coins;

    public event Action<int> OnChanged;

    public int Coins => coins;

    public CurrencyService()
    {
        Load();
    }

    void Load()
    {
        coins = PlayerPrefs.GetInt(COIN_KEY, 0);
    }

    void Save()
    {
        PlayerPrefs.SetInt(COIN_KEY, coins);
        PlayerPrefs.Save();
    }

    public void Add(int amount)
    {
        coins += amount;
        Save();
        OnChanged?.Invoke(coins);
    }

    public bool Spend(int amount)
    {
        if (coins < amount) return false;

        coins -= amount;
        Save();
        OnChanged?.Invoke(coins);
        return true;
    }
}
