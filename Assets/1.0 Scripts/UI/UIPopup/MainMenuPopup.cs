using EasyTransition;
using JS;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UnityEngine.UI;

namespace js {
    public class MainMenuPopup : UIBase
    {
        [Header("Transition")]
        [SerializeField] private TransitionSettings transitionSettings;
        [SerializeField] private float startDelay = 0f;
        [SerializeField] private Button OnPlayButton;
        [SerializeField] private Button OnShopButton;
        [SerializeField] private Button OnGunShopButton;
        [SerializeField] private Button OnExitButton;

        public static System.Action OnGunShopRequested;
        private const string MAIN_GAMEPLAY_SCENE = "MainGamePlay";

        private IUIService uiService;

        [Inject]
        public void Construct(IUIService uiService)
        {
            this.uiService = uiService;
        }

        private void Awake()
        {
            base.Awake();
            OnPlayButton?.onClick.AddListener(OnPlayButtonClicked);
            OnShopButton?.onClick.AddListener(OnShopButtonClicked);
            OnGunShopButton?.onClick.AddListener(OnGunShop);
            OnExitButton?.onClick.AddListener(OnQuitButtonClicked);
        }

        private void Start()
        {
            uiService.Show<MainMenuPopup>();
        }

        public void OnPlayButtonClicked()
        {
            TransitionManager.Instance().Transition(
                MAIN_GAMEPLAY_SCENE,
                transitionSettings,
                startDelay
            );
        }

        public void OnShopButtonClicked()
        {
            uiService.Hide<MainMenuPopup>();
            uiService.Show<UIShopPopup>();
        }

        public void OnGunShop()
        {
            OnGunShopRequested?.Invoke();
            uiService.Hide<MainMenuPopup>();
            uiService.Show<UIGunShopPopup>();
        }


        public void OnQuitButtonClicked()
        {
            Application.Quit();
        }
    }
}
