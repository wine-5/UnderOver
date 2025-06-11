using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// SEの名前と音源を保持するクラス
/// </summary>
[System.Serializable]
public class SEData
{
    public string name; /* SEの名前 */
    public AudioClip clip; /* 対応するSEの音源 */
}

/// <summary>
/// BGMとSEを管理し、再生するオーディオ管理クラス
/// </summary>
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    private const float SE_VOLUME_MULTIPLIER = 3f; /* SEの音量を倍 */

    [Header("オーディオソース")]
    [SerializeField] private AudioSource bgmSource; /* BGMを再生するAudioSource */
    [SerializeField] private AudioSource seSource; /* SEを再生するAudioSource */
    
    [Header("音声設定")]
    [SerializeField, Range(0f, 1f)] private float bgmVolume; /* BGMの音量 */
    [SerializeField, Range(0f, 1f)] private float seVolume; /* SEの音量 */

    [Header("BGMクリップ")]
    [SerializeField] private AudioClip titleBGM; /* タイトルBGM */
    [SerializeField] private AudioClip stage1BGM; /* ステージ1のBGM */
    [SerializeField] private AudioClip stage2BGM; /* ステージ2のBGM */
    [SerializeField] private AudioClip stage3BGM; /* ステージ3のBGM */
    [SerializeField] private AudioClip clearBGM;  /* ゲームクリア時のBGM */
    [SerializeField] private AudioClip gameOverBGM; /* ゲームオーバー時のBGM */

    [Header("SEクリップ")]
    [SerializeField] private List<SEData> seList = new(); /* インスペクターで設定するSEの一覧 */
    private Dictionary<String, AudioClip> seDict = new(); /* 実際に再生時に使用する辞書 */

    /// <summary>
    /// インスタンスを設定し、SE辞書を初期化する
    /// </summary>
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

    /// <summary>
    /// 指定した名前のBGMを再生する
    /// </summary>
    /// <param name="name">再生するBGMの名前</param>
    /// <param name="volume">音量（デフォルトは1.0f）</param>
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

    /// <summary>
    /// BGMの再生を停止する
    /// </summary>
    public void StopBGM()
    {
        bgmSource.Stop();
    }

    /// <summary>
    /// 指定した名前のSEを再生する
    /// </summary>
    /// <param name="name">再生するSEの名前</param>
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

    /// <summary>
    /// 名前に対応するBGMのAudioClipを取得する
    /// </summary>
    /// <param name="name">取得するBGMの名前</param>
    /// <returns>対応するBGMのAudioClip</returns>
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

    /// <summary>
    /// BGMの音量を取得および設定するプロパティ
    /// </summary>
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

    /// <summary>
    /// SEの音量を取得および設定するプロパティ
    /// </summary>
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
