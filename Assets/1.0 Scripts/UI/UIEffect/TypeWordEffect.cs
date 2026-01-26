using TMPro;
using UnityEngine;
using System.Collections;
public class TypeWordEffect : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI targetText;
    [SerializeField] private float wordDelay = 0.3f;

    private Coroutine runningCoroutine;

    public void Play(string fullText)
    {
        if (runningCoroutine != null)
            StopCoroutine(runningCoroutine);

        runningCoroutine = StartCoroutine(TypeEffect(fullText));
    }

    private IEnumerator TypeEffect(string text)
    {
        targetText.text = "";
        string[] words = text.Split(' ');

        for (int i = 0; i < words.Length; i++)
        {
            targetText.text += words[i];

            if (i < words.Length - 1)
                targetText.text += " ";

            yield return new WaitForSecondsRealtime(wordDelay);
        }

        runningCoroutine = null;
    }
}