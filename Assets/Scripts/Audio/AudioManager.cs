using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("オーディオソース")]
    [SerializeField] private AudioSource bgmSource;
    [SerializeField] private AudioSource seSource;

    [Header("BGMクリップ")]
    [SerializeField] private AudioClip titleBGM;
    [SerializeField] private AudioClip stage1BGM;
    [SerializeField] private AudioClip stage2BGM;
    [SerializeField] private AudioClip stage3BGM;
    [SerializeField] private AudioClip clearBGM;
    [SerializeField] private AudioClip gameOverBGM;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); /* シーンを超えて残す */
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayBGM(string name, float volume = 1.0f)
    {
        AudioClip clip = GetBGMClip(name);
        if (clip != null && bgmSource.clip != clip)
        {
            bgmSource.clip = clip;
            bgmSource.volume = volume;
            bgmSource.Play();
        }
    }

    public void StopBGM()
    {
        bgmSource.Stop();
    }

    public void PlaySE(AudioClip clip)
    {
        if (clip != null)
        {
            seSource.PlayOneShot(clip);
        }
    }

    private AudioClip GetBGMClip(string name)
    {
        return name switch
        {
            "Title" => titleBGM,
            "Stage1" => stage1BGM,
            "Stage2" => stage2BGM,
            "Stage3" => stage3BGM,
            "Clear" => clearBGM,
            "GameOver" => gameOverBGM,
            _ => null
        };
    }

    /* 音量調節用 */
    public void SetBGMVolume(float volume) => bgmSource.volume = volume;
    public void SetSEVolume(float volume) => seSource.volume = volume;
}
