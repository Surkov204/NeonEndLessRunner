using UnityEngine;
using JS;
using UnityEngine.UI;
using Zenject;
using TMPro;
using js;

public class SettingPopup : UIBase
{
    [Header("Sliders")]
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    [Header("Value Text")]
    [SerializeField] private TMP_Text musicValueText;
    [SerializeField] private TMP_Text sfxValueText;

    [Header("Buttons")]
    [SerializeField] private Button OnExitSetting;

    private const string MUSIC_KEY = "MusicVolume";
    private const string SFX_KEY = "SfxVolume";

    private IUIService uiService;

    [Inject]
    public void Construct(IUIService uiService)
    {
        this.uiService = uiService;
    }

    protected override void Awake()
    {
        base.Awake();

        float musicValue = PlayerPrefs.GetFloat(MUSIC_KEY, 1f);
        float sfxValue = PlayerPrefs.GetFloat(SFX_KEY, 1f);

        musicSlider.value = musicValue;
        sfxSlider.value = sfxValue;

        UpdateMusicText(musicValue);
        UpdateSfxText(sfxValue);

        musicSlider.onValueChanged.AddListener(OnMusicChanged);
        sfxSlider.onValueChanged.AddListener(OnSfxChanged);

        OnExitSetting.onClick.AddListener(OnExitSettingClick);
    }

    private void OnMusicChanged(float v)
    {
        AudioManager.Instance.SetMusicVolume(v);
        PlayerPrefs.SetFloat(MUSIC_KEY, v);
        UpdateMusicText(v);
    }

    private void OnSfxChanged(float v)
    {
        AudioManager.Instance.SetSFXVolume(v);
        PlayerPrefs.SetFloat(SFX_KEY, v);
        UpdateSfxText(v);
    }

    private void UpdateMusicText(float value)
    {
        // 0.4 hoặc 40%
        musicValueText.text = $"{(value * 100f):0}%";
        // nếu muốn dạng 0.4 → $"Music: {value:0.0}"
    }

    private void UpdateSfxText(float value)
    {
        sfxValueText.text = $"{(value * 100f):0}%";
    }

    private void OnExitSettingClick()
    {
        uiService.Hide<SettingPopup>();
        uiService.Show<MainMenuPopup>();
    }
}
