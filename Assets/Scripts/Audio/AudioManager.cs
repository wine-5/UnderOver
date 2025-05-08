using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class SEData
{
    public string name; /* SEの名前 */
    public AudioClip clip; /* 対応するSEの音源 */
}
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    private const float SE_VOLUME_MULTIPLIER = 3f; /* SEの音量を倍 */

    [Header("オーディオソース")]
    [SerializeField] private AudioSource bgmSource;
    [SerializeField] private AudioSource seSource;
    [Header("音声設定")]
    [SerializeField, Range(0f, 1f)] private float seVolume;
    [SerializeField, Range(0f, 1f)] private float bgmVolume;

    [Header("BGMクリップ")]
    [SerializeField] private AudioClip titleBGM;
    [SerializeField] private AudioClip stage1BGM;
    [SerializeField] private AudioClip stage2BGM;
    [SerializeField] private AudioClip stage3BGM;
    [SerializeField] private AudioClip clearBGM;
    [SerializeField] private AudioClip gameOverBGM;

    [Header("SEクリップ")]
    [SerializeField] private List<SEData> seList = new(); /* インスペクターで設定するSEの一覧 */
    private Dictionary<String, AudioClip> seDict = new(); /* 実際に再生時に使用する辞書 */

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); /* シーンを超えて残す */

            /* 辞書にSEを登録 */
            foreach (var se in seList)
            {
                seDict[se.name] = se.clip;
            }
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
            bgmSource.volume = BGMVolume;
            bgmSource.Play();
        }
    }

    public void StopBGM()
    {
        bgmSource.Stop();
    }

    public void PlaySE(string name)
    {
        if (seDict.TryGetValue(name, out var clip) && clip != null)
        {
            seSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning($"SEが見つかりません：{name}");
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



    /* BGMとSEの音量を格納するプロパティ */
    public float BGMVolume
    {
        get => bgmVolume;
        set
        {
            bgmVolume = Mathf.Clamp01(value); /* 0～1の間に制限する */
            bgmSource.volume = bgmVolume;      /* AudioSourceに反映する */
            Debug.Log($"今のBGMの音量は：{AudioManager.Instance.BGMVolume}");
        }
    }

    public float SEVolume
    {
        get => seVolume;
        set
        {
            seVolume = Mathf.Clamp01(value) * SE_VOLUME_MULTIPLIER; /* SEの音量を倍にして返す */
            seSource.volume = seVolume;      /* AudioSourceに反映する */
            Debug.Log($"SE音量が変更されました: {seVolume}");  // デバッグログを出力
        }
    }

}
