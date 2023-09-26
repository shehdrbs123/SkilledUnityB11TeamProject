using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;        // 후에 gameManager 사용하면서 싱글톤 해제

    [SerializeField] [Range(0f, 1f)] private float soundEffectVolume;
    [SerializeField] [Range(0f, 1f)] private float soundEffectPitchVariance;
    [SerializeField] [Range(0f, 1f)] private float bgmVolume;

    private PrefabManager prefabManager;

    private AudioSource bgmAudioSource;
    public AudioClip bgmClip;

    private void Awake()
    {
        instance = this;

        bgmAudioSource = GetComponent<AudioSource>();
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
        instance.bgmAudioSource.Stop();
        instance.bgmAudioSource.clip = music;
        instance.bgmAudioSource.Play();
    }

    public static void PlayClip(AudioClip clip)
    {
        GameObject obj = instance.prefabManager.SpawnFromPool(PoolType.SFXAudio);
        obj.SetActive(true);
        obj.GetComponent<SoundSource>().Play(clip, instance.soundEffectVolume, instance.soundEffectPitchVariance);
    }
}
