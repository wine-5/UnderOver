using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class VolumeSettings : MonoBehaviour
{
    
     /* ユーザーが自由に音量を変えるためのSlider */
     [SerializeField] private Slider bgmSlider;
     [SerializeField] private Slider seSlider;
      void Start()
    {
        /* スライダーの初期設定 */
        bgmSlider.value = AudioManager.Instance.BGMVolume;
        seSlider.value = AudioManager.Instance.SEVolume;

        /* スライダーが動いたときに音量を変更するイベントを登録 */
        bgmSlider.onValueChanged.AddListener(SetBGMVolume);
        seSlider.onValueChanged.AddListener(SetSEVolume);
    }

    void SetBGMVolume(float value)
    {
        AudioManager.Instance.BGMVolume = value; /* スライダーの値をAudioManagerに反映 */
    }

    void SetSEVolume(float value)
    {
        AudioManager.Instance.SEVolume = value; /* スライダーの値をAudioManagerに反映 */
    }
}
