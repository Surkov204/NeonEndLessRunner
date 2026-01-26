using System.Collections.Generic;
using UnityEngine;
using JS;

public class GunShowcaseController : MonoBehaviour
{
    [SerializeField] private List<GameObject> gunModels;

    private int lastOwnedPreviewIndex;
    private int currentPreviewIndex;
    private int equippedIndex;

    private void Awake()
    {
        equippedIndex = Mathf.Clamp(
            GunState.GetEquipped(),
            0,
            gunModels.Count - 1
        );

        currentPreviewIndex = equippedIndex;
        lastOwnedPreviewIndex = equippedIndex;

        SetActiveGun(equippedIndex);
    }

    private void OnEnable()
    {
        UIGunShopPopup.OnGunPreviewed += PreviewGun;
        UIGunShopPopup.OnGunEquipped += EquipGun;
        UIGunShopPopup.OnGunShopBackRequested += RestoreEquipped;
    }

    private void OnDisable()
    {
        UIGunShopPopup.OnGunPreviewed -= PreviewGun;
        UIGunShopPopup.OnGunEquipped -= EquipGun;
        UIGunShopPopup.OnGunShopBackRequested -= RestoreEquipped;
    }

    private void PreviewGun(int index)
    {
        if (index < 0 || index >= gunModels.Count)
            return;

        if (index == currentPreviewIndex)
            return;

        gunModels[currentPreviewIndex].SetActive(false);
        gunModels[index].SetActive(true);
        currentPreviewIndex = index;

        if (GunState.IsUnlocked(index))
        {
            lastOwnedPreviewIndex = index;
        }
    }

    private void EquipGun(int index)
    {
        equippedIndex = index;
        lastOwnedPreviewIndex = index;
        currentPreviewIndex = index;

        GunState.SetEquipped(index);
        SetActiveGun(index);
    }

    private void RestoreEquipped()
    {
        SetActiveGun(lastOwnedPreviewIndex);

        equippedIndex = lastOwnedPreviewIndex;
        currentPreviewIndex = equippedIndex;

        GunState.SetEquipped(equippedIndex);
    }

    private void SetActiveGun(int index)
    {
        for (int i = 0; i < gunModels.Count; i++)
        {
            gunModels[i].SetActive(i == index);
        }
    }
}
