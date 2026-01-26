using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ReloadRadialUI : MonoBehaviour
{
    [SerializeField] private Image reloadFill;
    private Coroutine fillRoutine;

    private void Start()
    {
        reloadFill.fillAmount = 1f;
    }

    public void StartFill(float duration)
    {
        duration = Mathf.Max(0.1f, duration); // tránh duration = 0

        reloadFill.fillAmount = 0f;

        if (fillRoutine != null)
            StopCoroutine(fillRoutine);

        fillRoutine = StartCoroutine(FillRoutine(duration));
    }

    private IEnumerator FillRoutine(float duration)
    {
        float t = 0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            reloadFill.fillAmount = t / duration;
            yield return null;
        }

        reloadFill.fillAmount = 1f;
        fillRoutine = null;
    }
}
