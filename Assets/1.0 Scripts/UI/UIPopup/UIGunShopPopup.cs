using js;
using UnityEngine;
using Zenject;
using UnityEngine.UI;
using TMPro;

namespace JS
{
    public class UIGunShopPopup : UIBase
    {
        [SerializeField] private GunDatabase gunDatabase;
        [SerializeField] private UICarousel carousel;
        [SerializeField] private Button buyButton;
        [SerializeField] private TextMeshProUGUI priceText;

        public static System.Action<int> OnGunPreviewed;
        public static System.Action<int> OnGunEquipped;
        public static System.Action OnGunShopBackRequested;

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
            carousel.SetIndex(GunState.GetEquipped(), true);
            carousel.OnIndexChanged += HandleIndexChanged;
            HandleIndexChanged(carousel.CurrentIndex);
        }

        private void OnDisable()
        {
            carousel.OnIndexChanged -= HandleIndexChanged;
        }

        private void HandleIndexChanged(int index)
        {
            OnGunPreviewed?.Invoke(index);

            if (!gunDatabase.IsShopItem(index) || GunState.IsUnlocked(index))
            {
                buyButton.gameObject.SetActive(false);
                return;
            }

            int price = gunDatabase.GetPrice(index);
            priceText.text = price.ToString();
            buyButton.gameObject.SetActive(true);

            buyButton.onClick.RemoveAllListeners();
            buyButton.onClick.AddListener(() => BuyGun(index, price));
        }

        private void BuyGun(int index, int price)
        {
            if (!currencyService.Spend(price))
                return;

            GunState.Unlock(index);
            GunState.SetEquipped(index);
            buyButton.gameObject.SetActive(false);

            OnGunEquipped?.Invoke(index);
        }

        public void BackButon()
        {
            OnGunShopBackRequested?.Invoke();
            uiService.Hide<UIGunShopPopup>();
            uiService.Show<MainMenuPopup>();
        }
    }
}
