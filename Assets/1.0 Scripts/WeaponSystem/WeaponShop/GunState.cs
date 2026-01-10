using UnityEngine;

public static class GunState
{
    private const string EQUIPPED_KEY = "GUN_EQUIPPED";

    public static int GetEquipped()
    {
        return PlayerPrefs.GetInt(EQUIPPED_KEY, 0);
    }

    public static void SetEquipped(int index)
    {
        PlayerPrefs.SetInt(EQUIPPED_KEY, index);
        PlayerPrefs.Save();
    }

    public static bool IsUnlocked(int index)
    {
        if (index == 0) return true; // súng mặc định
        return PlayerPrefs.GetInt("GUN_UNLOCKED_" + index, 0) == 1;
    }

    public static void Unlock(int index)
    {
        PlayerPrefs.SetInt("GUN_UNLOCKED_" + index, 1);
        PlayerPrefs.Save();
    }
}
