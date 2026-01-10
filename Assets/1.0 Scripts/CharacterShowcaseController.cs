using System.Collections.Generic;
using UnityEngine;
using js;

public class CharacterShowcaseController : MonoBehaviour
{
    [SerializeField] private List<GameObject> characterModels;

    private int lastOwnedPreviewIndex;
    private int currentPreviewIndex;
    private int equippedIndex;

    private void Awake()
    {
        equippedIndex = Mathf.Clamp(
            SkinState.GetEquipped(),
            0,
            characterModels.Count - 1
        );

        currentPreviewIndex = equippedIndex;
        lastOwnedPreviewIndex = equippedIndex;

        SetActiveCharacter(equippedIndex);
    }

    private void OnEnable()
    {
        UIShopPopup.OnSkinPreviewed += PreviewCharacter;
        UIShopPopup.OnSkinEquipped += EquipCharacter;
        UIShopPopup.OnShopClosed += RestoreEquipped;
    }

    private void OnDisable()
    {
        UIShopPopup.OnSkinPreviewed -= PreviewCharacter;
        UIShopPopup.OnSkinEquipped -= EquipCharacter;
        UIShopPopup.OnShopClosed -= RestoreEquipped;
    }

    private void PreviewCharacter(int index)
    {
        if (index < 0 || index >= characterModels.Count)
            return;

        if (index == currentPreviewIndex)
            return;

        // Preview model
        characterModels[currentPreviewIndex].SetActive(false);
        characterModels[index].SetActive(true);
        currentPreviewIndex = index;

        // 🔥 NẾU SKIN ĐÃ UNLOCK → GHI NHỚ LÀ "LAST OWNED"
        if (SkinState.IsUnlocked(index))
        {
            lastOwnedPreviewIndex = index;
        }
    }

    private void EquipCharacter(int index)
    {
        equippedIndex = index;
        lastOwnedPreviewIndex = index;
        currentPreviewIndex = index;

        SetActiveCharacter(index);
    }

    public void RestoreEquipped()
    {
        // 🔥 ƯU TIÊN lastOwnedPreviewIndex
        SetActiveCharacter(lastOwnedPreviewIndex);

        equippedIndex = lastOwnedPreviewIndex;
        SkinState.SetEquipped(equippedIndex);

        currentPreviewIndex = equippedIndex;
    }

    private void SetActiveCharacter(int index)
    {
        for (int i = 0; i < characterModels.Count; i++)
        {
            characterModels[i].SetActive(i == index);
        }
    }
}
