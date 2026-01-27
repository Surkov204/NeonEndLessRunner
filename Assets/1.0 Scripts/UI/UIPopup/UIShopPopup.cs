using JS;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace js
{
    public class UIShopPopup : UIBase
    {
        [SerializeField] private UICarousel carousel;
        [SerializeField] private Button buyButton;
        [SerializeField] private SkinDatabase skinDatabase;
        [SerializeField] private TextMeshProUGUI priceText;
        [SerializeField] private TextMeshProUGUI healthRegenUI;
        [SerializeField] private TextMeshProUGUI healthText;
        [SerializeField] private TextMeshProUGUI speedText;

        public static System.Action<int> OnSkinSelected;
        public static System.Action<int> OnSkinPreviewed;
        public static System.Action<int> OnSkinEquipped;
        public static System.Action OnShopClosed;

        private IUIService uiService;
        private ICurrencyService currencyService;

        [Inject]
        public void Construct(IUIService uiService, ICurrencyService currencyService)
        {
            this.uiService = uiService;
            this.currencyService = currencyService;
        }

        private void OnEnable()
        {
            int equippedIndex = SkinState.GetEquipped();
            carousel.SetIndex(equippedIndex, true);
            carousel.OnIndexChanged += HandleIndexChanged;
            HandleIndexChanged(carousel.CurrentIndex);
        }

        private void OnDisable()
        {
            carousel.OnIndexChanged -= HandleIndexChanged;
        }

        private void UpdateHealthUI(int index)
        {
            int hp = skinDatabase.GetHealth(index);
            healthText.text = $"{hp}";
        }

        private void UpdateHealthRegenUI(int index) {
            int hpRegen = skinDatabase.GetHealthRegen(index);
            healthRegenUI.text = $"{hpRegen}";
        }

        private void UpdateSpeedMovingUI(int index)
        {
            int speedMove = skinDatabase.GetSpeedMove(index);
            speedText.text = $"{speedMove}";
        }

        private void HandleIndexChanged(int index)
        {
            UpdateHealthUI(index);
            UpdateHealthRegenUI(index);
            UpdateSpeedMovingUI(index);
            SkinState.SetSelected(index);
            OnSkinPreviewed?.Invoke(index);

            if (!skinDatabase.IsShopItem(index))
            {
                buyButton.gameObject.SetActive(false);
                return;
            }

            if (SkinState.IsUnlocked(index))
            {
                buyButton.gameObject.SetActive(false);
                return;
            }

            int price = skinDatabase.GetPrice(index);
            buyButton.gameObject.SetActive(true);
            priceText.text = price.ToString();

            buyButton.onClick.RemoveAllListeners();
            buyButton.onClick.AddListener(() => BuySkin(index, price));
        }

        private void BuySkin(int index, int price)
        {
            if (!currencyService.Spend(price))
            {
                Debug.Log("Not enough coins");
                return;
            }

            SkinState.Unlock(index);
            SkinState.SetEquipped(index);
            HandleIndexChanged(index);
            OnSkinEquipped?.Invoke(index);
        }

        public void BackButon()
        {
            uiService.Hide<UIShopPopup>();

            OnShopClosed?.Invoke();

            uiService.Show<MainMenuPopup>();
        }

    }
}
