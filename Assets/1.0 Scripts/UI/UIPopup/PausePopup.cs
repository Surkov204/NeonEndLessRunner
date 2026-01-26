using EasyTransition;
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
        [SerializeField] private TransitionSettings transitionSettings;
        [SerializeField] private float startDelay = 0f;

        private const string MAIN_MENU_SCENE = "MainMenu";
        private const string MAIN_GAMEPLAY_SCENE = "MainGamepLay";

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
            TransitionManager.Instance().Transition(
             MAIN_GAMEPLAY_SCENE,
             transitionSettings,
             startDelay
           );
        }

        private void OnMenu()
        {
            Time.timeScale = 1f;
            TransitionManager.Instance().Transition(
              MAIN_MENU_SCENE,
              transitionSettings,
              startDelay
            );
        }
    }
}
