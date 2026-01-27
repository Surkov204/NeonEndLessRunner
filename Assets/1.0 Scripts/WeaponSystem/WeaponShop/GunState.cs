using UnityEngine;

public static class GunState
{
    private const string EQUIPPED_KEY = "GUN_EQUIPPED";
    private const string SELECTED_KEY = "GUN_SELECTED";

    public static int GetEquipped()
    {
        return PlayerPrefs.GetInt(EQUIPPED_KEY, 0);
    }

    public static void SetEquipped(int index)
    {
        PlayerPrefs.SetInt(EQUIPPED_KEY, index);
        PlayerPrefs.Save();
    }

    public static int GetSelected()
    {
        return PlayerPrefs.GetInt(SELECTED_KEY, GetEquipped());
    }

    public static void SetSelected(int index)
    {
        PlayerPrefs.SetInt(SELECTED_KEY, index);
    }

    public static bool IsUnlocked(int index)
    {
        if (index == 0) return true; 
        return PlayerPrefs.GetInt("GUN_UNLOCKED_" + index, 0) == 1;
    }

    public static void Unlock(int index)
    {
        PlayerPrefs.SetInt("GUN_UNLOCKED_" + index, 1);
        PlayerPrefs.Save();
    }

    public static void ResetSelectedToEquipped()
    {
        SetSelected(GetEquipped());
    }
}
