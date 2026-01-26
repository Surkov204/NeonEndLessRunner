using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DamageScreenUI : MonoBehaviour
{
    [Header("Images")]
    [SerializeField] private Image upDamage;
    [SerializeField] private Image downDamage;

    [Header("Timing")]
    [SerializeField] private float fadeInTime = 0.1f;
    [SerializeField] private float fadeOutTime = 0.6f;
    [SerializeField] private float maxAlpha = 0.8f;

    private Coroutine runningCoroutine;

    private void Awake()
    {
        SetAlpha(0f);
    }

    private void OnEnable()
    {
        PlayerDamageEvent.OnPlayerDamaged += PlayDamageEffect;
    }

    private void OnDisable()
    {
        PlayerDamageEvent.OnPlayerDamaged -= PlayDamageEffect;
    }

    public void PlayDamageEffect()
    {
        if (runningCoroutine != null)
            StopCoroutine(runningCoroutine);

        runningCoroutine = StartCoroutine(DamageRoutine());
    }

    private IEnumerator DamageRoutine()
    {
        float t = 0f;
        while (t < fadeInTime)
        {
            t += Time.deltaTime;
            float a = Mathf.Lerp(0f, maxAlpha, t / fadeInTime);
            SetAlpha(a);
            yield return null;
        }
        t = 0f;
        while (t < fadeOutTime)
        {
            t += Time.deltaTime;
            float a = Mathf.Lerp(maxAlpha, 0f, t / fadeOutTime);
            SetAlpha(a);
            yield return null;
        }

        SetAlpha(0f);
        runningCoroutine = null;
    }

    private void SetAlpha(float alpha)
    {
        SetImageAlpha(upDamage, alpha);
        SetImageAlpha(downDamage, alpha);
    }

    private void SetImageAlpha(Image img, float a)
    {
        if (img == null) return;
        Color c = img.color;
        c.a = a;
        img.color = c;
    }
}
