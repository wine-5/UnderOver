using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// ボタンにホバー時のアニメーション（拡大・縮小）を適用するクラス
/// IPointerEnterHandler と IPointerExitHandler を利用してマウス操作を検出
/// </summary>
public class ButtonDesign : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("ボタンのアニメーションの設定")]
    [SerializeField] private float scaleFactor = 1.1f;
    [SerializeField] private float smoothTime = 0.2f;
    private Vector3 originalScale;
    private RectTransform rectTransform;

    /// <summary>
    /// 初期化処理。元のスケールを保存
    /// </summary>
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        originalScale = rectTransform.localScale;
    }

    /// <summary>
    /// マウスポインターがボタンに入った時に呼ばれる。ボタンを拡大。
    /// </summary>
    public void OnPointerEnter(PointerEventData eventData)
    {
        StopAllCoroutines();
        StartCoroutine(ScaleButton(rectTransform.localScale, originalScale * scaleFactor));
    }

    /// <summary>
    /// マウスポインターがボタンから離れた時に呼ばれる。元の大きさに戻す。
    /// </summary>
    public void OnPointerExit(PointerEventData eventData)
    {
        StopAllCoroutines();
        StartCoroutine(ScaleButton(rectTransform.localScale, originalScale));
    }

    /// <summary>
    /// ボタンをなめらかに拡大・縮小するコルーチン。
    /// </summary>
    private System.Collections.IEnumerator ScaleButton(Vector3 fromScale, Vector3 toScale)
    {
        float elapsedTime = 0f;

        while (elapsedTime < smoothTime)
        {
            rectTransform.localScale = Vector3.Lerp(fromScale, toScale, elapsedTime / smoothTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        rectTransform.localScale = toScale;
    }
}
