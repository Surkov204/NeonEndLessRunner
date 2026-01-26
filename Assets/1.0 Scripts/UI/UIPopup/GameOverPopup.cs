using EasyTransition;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace JS {
    public class GameOverPopup : UIBase
    {
        [SerializeField] private Button OnMainMenuButton;
        [SerializeField] private TransitionSettings transitionSettings;
        [SerializeField] private float startDelay = 0f;

        private IUIService uiService;
        private ICurrencyService currencyService;
        private const string MAIN_GAMEPLAY_SCENE = "MainMenu";

        [Inject]
        public void Construct(IUIService uiService, ICurrencyService currencyService)
        {
            this.uiService = uiService;
            this.currencyService = currencyService;
        }

        private void Awake()
        {
            base.Awake();
            OnMainMenuButton?.onClick.AddListener(OnMainMenuClick);

        }

        private void OnMainMenuClick() {
            uiService.Hide<GameOverPopup>();
            Time.timeScale = 1;
            TransitionManager.Instance().Transition(
            MAIN_GAMEPLAY_SCENE,
               transitionSettings,
               startDelay
           );
        }

    }
}

