using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// BGM音量スライダーの入力を補正してAudioManagerに反映するコントローラー
/// </summary>
public class VolumeSliderController : MonoBehaviour
{
    [SerializeField] private Slider bgmSliderController; /* BGM音量調整用スライダー */
    private const float BGM_VOLUME_CORRECTION = 0.2f; /* 音量補正用定数 */

    /// <summary>
    /// スライダーの初期設定
    /// </summary>
    private void Start()
    {
        bgmSliderController.value = 1.0f; /* スライダーを一番右にセット */
    }

    /// <summary>
    /// BGM音量が変更されたときに呼ばれる処理
    /// </summary>
    /// <param name="sliderValue">スライダーの現在の値</param>
    public void OnBGMVolumeChanged(float sliderValue)
    {
        float correctedVolume = sliderValue * BGM_VOLUME_CORRECTION; /* 音量補正を適用 */
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.BGMVolume = correctedVolume; /* AudioManagerに反映 */
            Debug.Log($"現在のBGM音量: {AudioManager.Instance.BGMVolume}");
        }
    }
}
