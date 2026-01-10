using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;
using UnityEngine.UI;

public static class CarouselState
{
    public static int ShopIndex = 0;
}
/// <summary>
/// Generic reusable UI Carousel for any kind of scrollable/slideable UI elements.
/// </summary>
public class UICarousel : MonoBehaviour
{
    [Header("Carousel Items")]
    public List<RectTransform> items;

    [Header("Config")]
    public float spacing = 320f;
    public float sideScale = 0.75f;
    public float centerScale = 1.1f;
    public float transitionDuration = 0.35f;

    [Header("Optional UI")]
    public Button nextButton;
    public Button prevButton;

    public int CurrentIndex { get; private set; }

    public System.Action<int> OnIndexChanged;

    bool isTransitioning = false;

    void Start()
    {
        RefreshUI(true);
        SetupButtons();
    }

    public void SetIndex(int index, bool instant = true)
    {
        CurrentIndex = Mathf.Clamp(index, 0, items.Count - 1);
        RefreshUI(instant);
    }

    public void Next()
    {
        if (isTransitioning) return;
        if (CurrentIndex < items.Count - 1)
        {
            CurrentIndex++;
            RefreshUI(false);
        }
    }

    public void Prev()
    {
        if (isTransitioning) return;
        if (CurrentIndex > 0)
        {
            CurrentIndex--;
            RefreshUI(false);
        }
    }

    void SetupButtons()
    {
        if (nextButton)
            nextButton.onClick.AddListener(Next);

        if (prevButton)
            prevButton.onClick.AddListener(Prev);

        UpdateNavButtons();
    }

    void RefreshUI(bool instant)
    {
        isTransitioning = !instant;

        for (int i = 0; i < items.Count; i++)
        {
            RectTransform item = items[i];
            int offset = i - CurrentIndex;
            Vector3 targetPos = new Vector3(offset * spacing, 0, 0);
            float scale = (offset == 0) ? centerScale : sideScale;
            float alpha = (offset == 0) ? 1f : 0.5f;

            CanvasGroup cg = item.GetComponent<CanvasGroup>();
            if (cg == null)
                cg = item.gameObject.AddComponent<CanvasGroup>();

            if (instant)
            {
                item.anchoredPosition = targetPos;
                item.localScale = Vector3.one * scale;
                cg.alpha = alpha;
            }
            else
            {
                item.DOAnchorPos(targetPos, transitionDuration).SetEase(Ease.OutCubic);
                item.DOScale(scale, transitionDuration).SetEase(Ease.OutBack);
                cg.DOFade(alpha, transitionDuration * 0.8f);
            }
        }

        DOVirtual.DelayedCall(transitionDuration, () =>
        {
            isTransitioning = false;
            OnIndexChanged?.Invoke(CurrentIndex);
            UpdateNavButtons();
        });
    }

    void UpdateNavButtons()
    {
        if (nextButton)
            nextButton.interactable = (CurrentIndex < items.Count - 1);
        if (prevButton)
            prevButton.interactable = (CurrentIndex > 0);
    }
}
