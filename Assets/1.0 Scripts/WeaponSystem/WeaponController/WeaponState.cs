using UnityEngine;

public static class WeaponState
{
    private const string EQUIPPED_WEAPON = "EQUIPPED_WEAPON";

    public static void SetEquipped(int index)
    {
        PlayerPrefs.SetInt(EQUIPPED_WEAPON, index);
        PlayerPrefs.Save();
    }

    public static int GetEquipped()
    {
        return PlayerPrefs.GetInt(EQUIPPED_WEAPON, 0);
    }
}
