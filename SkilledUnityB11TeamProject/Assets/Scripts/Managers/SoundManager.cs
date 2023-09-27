using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] [Range(0f, 1f)] private float soundEffectVolume;
    [SerializeField] [Range(0f, 1f)] private float soundEffectPitchVariance;
    [SerializeField] [Range(0f, 1f)] private float bgmVolume;

    private PrefabManager prefabManager;

    [SerializeField] private AudioSource bgmAudioSource;
    public AudioClip bgmClip;

    private void Awake()
    {
        bgmAudioSource.volume = bgmVolume;
        bgmAudioSource.loop = true;

        prefabManager = GetComponent<PrefabManager>();
    }

    private void Start()
    {
        ChangeBackGroundMusic(bgmClip);
    }

    public static void ChangeBackGroundMusic(AudioClip music)
    {
        GameManager.Instance._soundManager.bgmAudioSource.Stop();
        GameManager.Instance._soundManager.bgmAudioSource.clip = music;
        GameManager.Instance._soundManager.bgmAudioSource.Play();
    }

    public static void PlayClip(AudioClip clip)
    {
        GameObject obj = GameManager.Instance._soundManager.prefabManager.SpawnFromPool(PoolType.SFXAudio);
        obj.SetActive(true);
        obj.GetComponent<SoundSource>().Play(clip, GameManager.Instance._soundManager.soundEffectVolume, GameManager.Instance._soundManager.soundEffectPitchVariance);
    }
}
