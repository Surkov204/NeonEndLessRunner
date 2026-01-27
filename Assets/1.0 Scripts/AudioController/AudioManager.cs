using JS.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static SoundFXLibrary;


public class AudioManager : ManualSingletonMono<AudioManager>
{
    [SerializeField] private AudioSource setSfxVolume;
    [SerializeField] private AudioSource setMusicVolume;

    [Header("Scene → Music Config")]
    [SerializeField] private SceneMusicConfig musicConfig;
    [Header("Sound FX Library")]
    [SerializeField] private SoundFXLibrary sfxLibrary;

    private Dictionary<SoundFXName, AudioSource> loopSources = new();

    private const string MusicVolumeKey = "MusicVolume";
    private const string SfxVolumeKey = "SfxVolume";

    private void Awake()
    {
        base.Awake();

        if (Instance != this) return;

        float savedMusicVol = PlayerPrefs.GetFloat(MusicVolumeKey, 1f);
        float savedSfxVol = PlayerPrefs.GetFloat(SfxVolumeKey, 1f);

        SetMusicVolume(savedMusicVol);
        SetSFXVolume(savedSfxVol);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }


    private void OnDestroy()
    {
        if (Instance == this)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
        base.OnDestroy();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlayMusicForScene(scene.name);
    }

    public void PlayMusicForScene(string sceneName)
    {
        if (setMusicVolume == null || setMusicVolume == null) return;

        AudioClip clip = musicConfig.GetMusicForScene(sceneName);

        if (clip != null && setMusicVolume.clip != clip)
        {
            setMusicVolume.clip = clip;
            setMusicVolume.loop = true;
            setMusicVolume.Play();
        }
    }

    public void PlaySoundFX(SoundFXName soundName)
    {
        if (sfxLibrary == null) return;
        AudioClip clip = sfxLibrary.GetClip(soundName);
        if (clip != null)
            PlaySound(clip);
        else
            Debug.LogWarning($"[AudioManager] Sound '{soundName}' not found in SFX Library.");
    }

    public void PlayLoop(SoundFXName soundName)
    {
        if (loopSources.ContainsKey(soundName))
        {
            if (!loopSources[soundName].isPlaying)
                loopSources[soundName].Play();
            return;
        }

        AudioClip clip = sfxLibrary.GetClip(soundName);
        if (clip == null)
        {
            Debug.LogWarning($"[AudioManager] Loop sound '{soundName}' not found.");
            return;
        }

        // Tạo AudioSource mới cho tiếng loop này
        AudioSource src = gameObject.AddComponent<AudioSource>();
        src.clip = clip;
        src.loop = true;
        src.playOnAwake = false;
        src.volume = setSfxVolume.volume; // đồng bộ volume
        src.spatialBlend = 0f; // 2D
        src.Play();

        loopSources[soundName] = src;
    }

    public void StopLoop(SoundFXName soundName)
    {
        if (!loopSources.ContainsKey(soundName)) return;

        AudioSource src = loopSources[soundName];
        if (src != null && src.isPlaying)
            src.Stop();
    }

    public void StopAllLoops()
    {
        foreach (var kv in loopSources)
            if (kv.Value != null)
                kv.Value.Stop();
    }

    public bool IsLoopPlaying(SoundFXName soundName)
    {
        return loopSources.ContainsKey(soundName) && loopSources[soundName].isPlaying;
    }


    public void PlaySound(AudioClip _sound)
    {
        setSfxVolume.PlayOneShot(_sound);
    }

    public void SetMusicVolume(float value)
    {
        if (setMusicVolume != null) setMusicVolume.volume = value;
    }

    public void SetSFXVolume(float value)
    {
        if (setSfxVolume != null) setSfxVolume.volume = value;
    }

    public float GetMusicVolume() => setMusicVolume != null ? setMusicVolume.volume : 1f;
    public float GetSFXVolume() => setSfxVolume != null ? setSfxVolume.volume : 1f;
}
