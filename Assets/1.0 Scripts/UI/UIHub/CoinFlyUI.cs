using UnityEngine;
using DG.Tweening;
using Zenject;

public class CoinFlyUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private RectTransform canvasRect;
    [SerializeField] private RectTransform coinTarget;
    [SerializeField] private GameObject coinFlyPrefab;

    [Header("Tween Settings")]
    [SerializeField] private float flyTime = 0.6f;
    [SerializeField] private float spreadRadius = 120f;
    [SerializeField] private Ease finalEase = Ease.InOutQuad;

    [Header("Optional")]
    [SerializeField] private Camera uiCamera;

    private ICurrencyService currency;

    [Inject]
    void Construct(ICurrencyService currencyService)
    {
        currency = currencyService;
    }

    // ==============================
    // PUBLIC API
    // ==============================
    public void SpawnCoinFly(Vector3 worldPos, int amount)
    {
        if (!coinFlyPrefab || !coinTarget || !canvasRect)
            return;

        Vector2 startPos = WorldToCanvas(worldPos);
        Vector2 targetPos = RectTransformToCanvas(coinTarget);

        for (int i = 0; i < amount; i++)
        {
            SpawnSingleCoin(startPos, targetPos, i * 0.03f);
        }
    }

    // ==============================
    // CORE
    // ==============================
    private void SpawnSingleCoin(Vector2 startPos, Vector2 targetPos, float delay)
    {
        RectTransform coin = Instantiate(coinFlyPrefab, canvasRect)
            .GetComponent<RectTransform>();

        coin.anchoredPosition = startPos;
        coin.localScale = Vector3.one;

        Vector2 midPos = startPos + Random.insideUnitCircle * spreadRadius;

        Sequence seq = DOTween.Sequence();
        seq.SetDelay(delay);

        seq.Append(coin.DOAnchorPos(midPos, flyTime * 0.35f)
            .SetEase(Ease.OutQuad));

        seq.Append(coin.DOAnchorPos(targetPos, flyTime * 0.65f)
            .SetEase(finalEase));

        seq.OnComplete(() =>
        {
            currency.Add(1);   // ✅ cộng tiền CHUẨN
            Destroy(coin.gameObject);
        });
    }

    // ==============================
    // CONVERT
    // ==============================
    private Vector2 WorldToCanvas(Vector3 worldPos)
    {
        Vector2 screenPos = Camera.main.WorldToScreenPoint(worldPos);

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRect,
            screenPos,
            uiCamera,
            out Vector2 canvasPos
        );

        return canvasPos;
    }

    private Vector2 RectTransformToCanvas(RectTransform target)
    {
        Vector2 screenPos = RectTransformUtility.WorldToScreenPoint(
            uiCamera,
            target.position
        );

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRect,
            screenPos,
            uiCamera,
            out Vector2 canvasPos
        );

        return canvasPos;
    }
}
