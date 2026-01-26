using UnityEngine;

public static class SkinState
{
    private const string EQUIPPED_KEY = "SKIN_EQUIPPED";

    private static int selectedIndex = -1;

    public static int GetEquipped()
    {
        return PlayerPrefs.GetInt(EQUIPPED_KEY, 0);
    }

    public static void SetEquipped(int index)
    {
        PlayerPrefs.SetInt(EQUIPPED_KEY, index);
        PlayerPrefs.Save();

        selectedIndex = index; // sync khi equip
    }

    // 🔥 NEW
    public static int GetSelected()
    {
        return selectedIndex >= 0 ? selectedIndex : GetEquipped();
    }

    // 🔥 NEW
    public static void SetSelected(int index)
    {
        selectedIndex = index;
    }

    public static bool IsUnlocked(int index)
    {
        if (index == 0) return true;
        return PlayerPrefs.GetInt("SKIN_UNLOCKED_" + index, 0) == 1;
    }

    public static void Unlock(int index)
    {
        PlayerPrefs.SetInt("SKIN_UNLOCKED_" + index, 1);
        PlayerPrefs.Save();
    }
}
