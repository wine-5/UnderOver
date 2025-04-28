using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// BGM音量スライダーの入力を補正してAudioManagerに反映するコントローラー
/// </summary>
public class VolumeSliderController : MonoBehaviour
{
    [SerializeField] private Slider bgmSliderController;
    private const float BGM_VOLUME_CORRECTION = 0.2f;

    private void Start()
    {
        bgmSliderController.value = 1.0f; /* スライダーを一番右にセットする */
        
    }

    public void OnBGMVolumeChanged(float sliderValue)
    {
        float correctedVolume = sliderValue * BGM_VOLUME_CORRECTION;
        if(AudioManager.Instance != null)
        {
            AudioManager.Instance.BGMVolume = correctedVolume;
            Debug.Log($"今のBGMの音量は：{AudioManager.Instance.BGMVolume}");
        }
    }
}
