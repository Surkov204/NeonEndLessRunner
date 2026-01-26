using UnityEngine;
using DG.Tweening;
using js;
using JS;

namespace JS
{
    public class GunShopTransitionController : MonoBehaviour
    {
        [Header("Camera")]
        [SerializeField] private Camera mainCamera;
        [SerializeField] private float targetCameraZ = -14.5f;
        [SerializeField] private float cameraMoveDuration = 0.6f;

        [Header("Gun")]
        [SerializeField] private Transform gunTransform;
        [SerializeField] private float gunMoveY = 1.2f;
        [SerializeField] private float gunMoveDuration = 0.4f;

        [Header("UI Root")]
        [SerializeField] private RectTransform mainMenuRoot;
        [SerializeField] private float uiSlideDuration = 0.4f;

        private Vector3 cameraStartPos;
        private Vector3 gunLocalStartPos;
        private Vector2 uiStartPos;

        private void Awake()
        {
            cameraStartPos = mainCamera.transform.position;
            gunLocalStartPos = gunTransform.localPosition;
            uiStartPos = mainMenuRoot.anchoredPosition;
        }

        private void OnEnable()
        {
            MainMenuPopup.OnGunShopRequested += PlayEnter;
            UIGunShopPopup.OnGunShopBackRequested += PlayExit;
        }

        private void OnDisable()
        {
            MainMenuPopup.OnGunShopRequested -= PlayEnter;
            UIGunShopPopup.OnGunShopBackRequested -= PlayExit;
        }

        // ▶️ ENTER
        private void PlayEnter()
        {
            DOTween.Kill(this);

            Sequence seq = DOTween.Sequence().SetTarget(this);

            seq.Append(
                mainCamera.transform.DOMoveZ(targetCameraZ, cameraMoveDuration)
                    .SetEase(Ease.InOutCubic)
            );

            seq.Join(
                gunTransform.DOLocalMoveY(
                    gunLocalStartPos.y + gunMoveY,
                    gunMoveDuration
                ).SetEase(Ease.OutBack)
            );

            seq.Join(
                mainMenuRoot.DOAnchorPosX(
                    -mainMenuRoot.rect.width,
                    uiSlideDuration
                ).SetEase(Ease.InCubic)
            );

            seq.OnComplete(() =>
            {
                // Chỉ sau khi animation xong mới hide/show UI
                // uiService.Hide<MainMenuPopup>();
                // uiService.Show<UIGunShopPopup>();
            });
        }

        // ◀️ EXIT / BACK
        private void PlayExit()
        {
            DOTween.Kill(this);

            Sequence seq = DOTween.Sequence().SetTarget(this);

            seq.Append(
                mainCamera.transform.DOMove(cameraStartPos, cameraMoveDuration)
                    .SetEase(Ease.InOutCubic)
            );

            seq.Join(
                gunTransform.DOLocalMove(gunLocalStartPos, gunMoveDuration)
                    .SetEase(Ease.InCubic)
            );

            seq.Join(
                mainMenuRoot.DOAnchorPos(uiStartPos, uiSlideDuration)
                    .SetEase(Ease.OutCubic)
            );

            seq.OnComplete(() =>
            {
                // Sau khi reset xong mới đổi UI
                // uiService.Hide<UIGunShopPopup>();
                // uiService.Show<MainMenuPopup>();
            });
        }
    }
}
