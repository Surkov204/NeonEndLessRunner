using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using Zenject;

public class AmmoUI : MonoBehaviour
{
    [SerializeField] private Slider ammoSlider;
    [SerializeField] private TextMeshProUGUI ammoText;

    private SignalBus signalBus;
    private bool subscribed;

    [Inject]
    public void Construct(SignalBus signalBus)
    {
        this.signalBus = signalBus;
    }

    private void OnEnable()
    {
        StartCoroutine(DeferredSubscribe());
    }

    private IEnumerator DeferredSubscribe()
    {
        yield return null; // đợi 1 frame cho chắc inject xong
        if (signalBus == null || subscribed) yield break;

        signalBus.Subscribe<AmmoChangedSignal>(OnAmmoChanged);
        subscribed = true;
    }

    private void OnDisable()
    {
        if (signalBus == null || !subscribed) return;
        signalBus.Unsubscribe<AmmoChangedSignal>(OnAmmoChanged);
        subscribed = false;
    }

    private void OnAmmoChanged(AmmoChangedSignal s)
    {
        ammoSlider.maxValue = s.Max;
        ammoSlider.value = s.Current;
        ammoText.text = $"{s.Current} / {s.Max}";
    }
}
