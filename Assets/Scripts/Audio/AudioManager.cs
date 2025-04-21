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

    [Header("SEクリップ")]
    [SerializeField] private List<SEData> seList = new(); /* インスペクターで設定するSEの一覧 */
    private Dictionary<String, AudioClip> seDict = new(); /* 実際に再生時に使用する辞書 */

    /* SEクリップ（辞書）の使い方
     * インスペクターからListを開いて名前とSEを入れる
     * VSCで呼び出したいメソッドに以下のように書く
     * AudioManager.Instance.PlaySE("ここにSEの名前");
     */

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); /* シーンを超えて残す */

            /* 辞書にSEを登録 */
            foreach(var se in seList)
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
        if(seDict.TryGetValue(name,out var clip) && clip != null)
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
        get => bgmSource.volume; /* 現在のBGM音量を返す */
        set => bgmSource.volume = value; /* 音量変更時にAudioSourceの音量を更新 */
   }

   public float SEVolume
   {
        get => seSource.volume; /* 現在のSE音量を返す */
        set => seSource.volume = value; /* 音量変更時にAudioSourceの音量を更新 */
   }
}
