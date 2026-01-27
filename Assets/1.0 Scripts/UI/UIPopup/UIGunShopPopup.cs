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
        [SerializeField] private TextMeshProUGUI gunNameText;
        [SerializeField] private TextMeshProUGUI maxAmmoText;
        [SerializeField] private TextMeshProUGUI fireRateText;
        [SerializeField] private TextMeshProUGUI reloadTimeText;
        [SerializeField] private TextMeshProUGUI muzzleVelocityText;

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
            GunState.ResetSelectedToEquipped();
            carousel.SetIndex(GunState.GetEquipped(), true);
            carousel.OnIndexChanged += HandleIndexChanged;
            HandleIndexChanged(carousel.CurrentIndex);
        }

        private void OnDisable()
        {
            carousel.OnIndexChanged -= HandleIndexChanged;
        }

        private void UpdateGunInfo(int index)
        {
            GunConfig gun = gunDatabase.GetGun(index);
            if (gun == null) return;

            gunNameText.text = gun.gunName;
            maxAmmoText.text = gun.maxAmmo.ToString();
            fireRateText.text = gun.fireRate.ToString("0.00");
            reloadTimeText.text = gun.reloadTime.ToString("0.0");
            muzzleVelocityText.text = gun.muzzleVelocity.ToString("0");
        }

        private void HandleIndexChanged(int index)
        {
            GunState.SetSelected(index);
            OnGunPreviewed?.Invoke(index);
            UpdateGunInfo(index);
            if (GunState.IsUnlocked(index))
            {
                GunState.SetEquipped(index);
                WeaponState.SetEquipped(index);  
                OnGunEquipped?.Invoke(index);

                buyButton.gameObject.SetActive(false);
                return;
            }

            if (gunDatabase.IsShopItem(index))
            {
                int price = gunDatabase.GetPrice(index);
                priceText.text = price.ToString();
                buyButton.gameObject.SetActive(true);

                buyButton.onClick.RemoveAllListeners();
                buyButton.onClick.AddListener(() => BuyGun(index, price));
            }
            else
            {
                buyButton.gameObject.SetActive(false);
            }
        }

        private void BuyGun(int index, int price)
        {
            if (!currencyService.Spend(price))
                return;

            GunState.Unlock(index);
            GunState.SetEquipped(index);
            WeaponState.SetEquipped(index);
            OnGunEquipped?.Invoke(index);
            HandleIndexChanged(index);
        }

        public void BackButon()
        {
            OnGunShopBackRequested?.Invoke();
            uiService.Hide<UIGunShopPopup>();
            uiService.Show<MainMenuPopup>();
        }
    }
}
