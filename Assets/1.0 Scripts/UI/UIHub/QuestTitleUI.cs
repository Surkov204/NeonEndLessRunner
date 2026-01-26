using UnityEngine;
using System.Collections;
using TMPro;

public class QuestTitleUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI questText;
    [SerializeField] private TypeWordEffect typeWordEffect;

    [Header("Position")]
    [SerializeField] private Vector2 startPos;
    [SerializeField] private Vector2 targetAnchoredPosition;

    [Header("Size")]
    [SerializeField] private float startSize = 150f;
    [SerializeField] private float endSize = 50f;

    [Header("Timing")]
    [SerializeField] private float moveDuration = 1.5f;
    [SerializeField] private float holdTime = 1.5f;

    private Coroutine runningCoroutine;

    private void Awake()
    {
        questText.gameObject.SetActive(false);
    }

    public void ShowQuestTitle(string text)
    {
        if (runningCoroutine != null)
            StopCoroutine(runningCoroutine);

        questText.gameObject.SetActive(true);
        questText.text = text;
        questText.fontSize = startSize;
        questText.rectTransform.anchoredPosition = startPos;

        typeWordEffect?.Play(text);

        runningCoroutine = StartCoroutine(PlaySequence());
    }

    public void HideQuestTitle()
    {
        if (runningCoroutine != null)
            StopCoroutine(runningCoroutine);

        questText.gameObject.SetActive(false);
        runningCoroutine = null;
    }

    private IEnumerator PlaySequence()
    {
        yield return new WaitForSeconds(0.8f);

        float timer = 0f;

        while (timer < moveDuration)
        {
            timer += Time.deltaTime;
            float t = Mathf.Clamp01(timer / moveDuration);

            questText.rectTransform.anchoredPosition =
                Vector2.Lerp(startPos, targetAnchoredPosition, t);

            questText.fontSize =
                Mathf.Lerp(startSize, endSize, t);

            yield return null;
        }

        yield return new WaitForSeconds(holdTime);
        runningCoroutine = null;
    }
}
