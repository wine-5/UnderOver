using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvincibilityController : MonoBehaviour
{
    /* Playerの基本設定 */
    [SerializeField] private float invincibleTime = 1.0f;
    [SerializeField] private float flashInterval = 0.1f;
    private SpriteRenderer spriteRenderer;

    /* フラグの設定 */
    private bool isInvincible = false;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void StartInvincibility()
    {
        if (!isInvincible)
        {
            StartCoroutine(InvincibilityRoutine());
        }
    }

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

    public bool IsInvincible()
    {
        return isInvincible;
    }
}
