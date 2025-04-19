using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonDesign : MonoBehaviour,IPointerEnterHandler, IPointerExitHandler
{
    [Header("ボタンのアニメーションの設定")]
    [SerializeField] private float scaleFactor = 1.1f;
    [SerializeField] private float smoothTime = 0.2f;
    private Vector3 originalScale;
    private RectTransform rectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        originalScale = rectTransform.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        StopAllCoroutines();
        StartCoroutine(ScaleButton(rectTransform.localScale, originalScale * scaleFactor));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StopAllCoroutines();
        StartCoroutine(ScaleButton(rectTransform.localScale, originalScale));
    }

    private System.Collections.IEnumerator ScaleButton(Vector3 fromScale, Vector3 toScale)
    {
        float elapsedTime = 0f;

        while(elapsedTime < smoothTime)
        {
            rectTransform.localScale = Vector3.Lerp(fromScale, toScale,elapsedTime / smoothTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        rectTransform.localScale = toScale;
    }
}
