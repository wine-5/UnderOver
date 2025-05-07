using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーがトリガーに入ったとき、指定されたとげオブジェクトを有効化するギミック用クラス。
/// </summary>
public class GimmickTrigger : MonoBehaviour
{
    [Header("とげの親オブジェクト")]
    [SerializeField] private GameObject spikesParent;
    private bool hasTriggered = false;

    /// <summary>
    /// プレイヤーがトリガーに入ったときに呼び出され、ギミックを一度だけ起動する。
    /// </summary>
    /// <param name="other">トリガーに侵入したコライダー。</param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasTriggered) return;

        if (other.CompareTag("Player"))
        {
            if (spikesParent != null)
            {
                spikesParent.SetActive(true);
                hasTriggered = true;
            }
            else
            {
                Debug.LogError("spikesParentがアタッチされていない");
            }
        }
    }
}
