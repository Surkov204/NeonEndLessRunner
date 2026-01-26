using System.Collections.Generic;
using UnityEngine;

public class PlayerModelController : MonoBehaviour
{
    [SerializeField] private List<GameObject> characterModels;
    [SerializeField] private Health health;

    private int currentIndex = -1;

    private void Awake()
    {
        for (int i = 0; i < characterModels.Count; i++)
        {
            characterModels[i].SetActive(false);
        }

        currentIndex = -1;
        int equippedIndex = SkinState.GetEquipped();

        ApplySkin(equippedIndex);
    }

    private void OnEnable()
    {
        js.UIShopPopup.OnSkinSelected += ApplySkin;
    }

    private void OnDisable()
    {
        js.UIShopPopup.OnSkinSelected -= ApplySkin;
    }

    private void ApplySkin(int index)
    {
        if (index < 0 || index >= characterModels.Count)
            return;

        if (currentIndex == index)
            return;

        if (currentIndex >= 0)
        {
            characterModels[currentIndex].SetActive(false);
        }

        GameObject activeSkin = characterModels[index];
        activeSkin.SetActive(true);
        currentIndex = index;

        SkinStatsHolder holder = activeSkin.GetComponent<SkinStatsHolder>();
        if (holder != null && holder.stats != null)
        {
            health.SetMaxHealth(holder.stats.maxHealth);
        }
    }
}
