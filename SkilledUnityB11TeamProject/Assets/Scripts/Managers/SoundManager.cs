using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SoundManager : MonoBehaviour
{
    [SerializeField] [Range(0f, 1f)] private float soundEffectVolume;
    [SerializeField] [Range(0f, 1f)] private float soundEffectPitchVariance;
    [SerializeField] [Range(0f, 1f)] private float bgmVolume;

    private PrefabManager prefabManager;
    public static Dictionary<string, AudioClip> dic { get; private set; }

    [SerializeField] private AudioSource bgmAudioSource;
    public AudioClip bgmClip;

    private void Awake()
    {
        bgmAudioSource.volume = bgmVolume;
        bgmAudioSource.loop = true;

        prefabManager = GetComponent<PrefabManager>();
        dic = new Dictionary<string, AudioClip>();

        AudioClip[] audios = Resources.LoadAll<AudioClip>("AudioData");

        foreach (AudioClip audio in audios)
        {
            dic.Add(audio.name, audio);
        }
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

    public static void PlayClip(AudioClip clip,Vector3 position)
    {
        GameObject obj = GameManager.Instance.prefabManager.SpawnFromPool(PoolType.SFXAudio);
        obj.SetActive(true);
        obj.transform.position = position;
        obj.GetComponent<SoundSource>().Play(clip, GameManager.Instance._soundManager.soundEffectVolume, GameManager.Instance._soundManager.soundEffectPitchVariance);
    }

    public static void PlayClip(string clipName,Vector3 position)
    {
        if (dic.TryGetValue(clipName, out AudioClip audio))
        {
            PlayClip(audio, position);            
        }
        else
        {
            Debug.Log($"{clipName}은 없는 사운드입니다 확인해 주세요");
        }
    }
}
