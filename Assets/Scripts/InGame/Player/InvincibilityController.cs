using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーの無敵状態を管理
/// </summary>
public class InvincibilityController : MonoBehaviour
{
    /* Playerの基本設定 */
    [SerializeField] private float invincibleTime = 1.0f;
    [SerializeField] private float flashInterval = 0.1f;
    private SpriteRenderer spriteRenderer;

    /* フラグの設定 */
    private bool isInvincible = false;

    /// <summary>
    /// SpriteRendererを取得する
    /// </summary>
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    /// <summary>
    /// 無敵状態を開始
    /// 無敵状態でなければ、無敵処理を開始する
    /// </summary>        
    public void StartInvincibility()
    {
        if (!isInvincible)
        {
            StartCoroutine(InvincibilityRoutine());
        }
    }

    /// <summary>
    /// 無敵処理を行うコルーチン
    /// 無敵時間中、スプライトが点滅
    /// </summary>
    /// <returns>コルーチンの実行</returns>

    private IEnumerator InvincibilityRoutine()
    {
        isInvincible = true;

        for (float elapsedTime = 0f; elapsedTime < invincibleTime; elapsedTime += flashInterval)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;
            yield return new WaitForSeconds(flashInterval);
        }

        spriteRenderer.enabled = true;
        isInvincible = false;
    }

    /// <summary>
    /// プレイヤーが無敵状態かどうかを返す。
    /// </summary>
    /// <returns>無敵状態かどうか</returns>
    public bool IsInvincible()
    {
        return isInvincible;
    }
}
