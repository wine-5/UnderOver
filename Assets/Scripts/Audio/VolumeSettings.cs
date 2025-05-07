using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ユーザーが音量を調整できるスライダー設定を管理するクラス
/// </summary>
public class VolumeSettings : MonoBehaviour
{
    /* ユーザーが自由に音量を変えるためのSlider */
    [SerializeField] private Slider bgmSlider; /* BGM音量調整用スライダー */
    [SerializeField] private Slider seSlider;  /* SE音量調整用スライダー */

    /// <summary>
    /// スライダーの初期設定とイベントの登録
    /// </summary>
    void Start()
    {
        /* スライダーの初期設定 */
        bgmSlider.value = AudioManager.Instance.BGMVolume; /* 初期値をAudioManagerから取得 */
        seSlider.value = AudioManager.Instance.SEVolume;   /* 初期値をAudioManagerから取得 */

        /* スライダーが動いたときに音量を変更するイベントを登録 */
        bgmSlider.onValueChanged.AddListener(SetBGMVolume);
        seSlider.onValueChanged.AddListener(SetSEVolume);
    }

    /// <summary>
    /// BGM音量を設定するメソッド
    /// </summary>
    /// <param name="value">設定する音量</param>
    void SetBGMVolume(float value)
    {
        AudioManager.Instance.BGMVolume = value; /* スライダーの値をAudioManagerに反映 */
    }

    /// <summary>
    /// SE音量を設定するメソッド
    /// </summary>
    /// <param name="value">設定する音量</param>
    void SetSEVolume(float value)
    {
        AudioManager.Instance.SEVolume = value; /* スライダーの値をAudioManagerに反映 */
    }
}
