using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

namespace JS
{
    public class PausePopup : UIBase
    {
        [Header("Buttons")]
        [SerializeField] private Button continueButton;
        [SerializeField] private Button retryButton;
        [SerializeField] private Button menuButton;

        private IUIService uiService;

        [Inject]
        public void Construct(IUIService uiService)
        {
            this.uiService = uiService;
        }

        private void Awake()
        {
            base.Awake();

            if (continueButton != null)
                continueButton.onClick.AddListener(OnContinue);

            if (retryButton != null)
                retryButton.onClick.AddListener(OnRetry);

            if (menuButton != null)
                menuButton.onClick.AddListener(OnMenu);
        }

        private void OnContinue()
        {
            Time.timeScale = 1f;
            EventSystem.current.SetSelectedGameObject(null);
            uiService.Hide<PausePopup>();
        }

        private void OnRetry()
        {
            Time.timeScale = 1f;
            Debug.Log("[PausePopup] Retry");
        }

        private void OnMenu()
        {
            Time.timeScale = 1f;
            Debug.Log("[PausePopup] Menu");
        }
    }
}
