using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// プレイヤーのHP管理を行い、ダメージを受けた際の処理や無敵化を制御
/// </summary>
public class PlayerManager : MonoBehaviour
{
    /* プレイヤーのHP */
    [SerializeField] private int currentHP = 3;
    [SerializeField] private List<Image> hpIcons; /* HPアイコンを入れる変数 */
    private InvincibilityController invincibilityController; /* InvincibilityController（無敵状態のスクリプト）を参照する */

    /// <summary>
    ///  現在のPlayerのHPを取得する
    /// </summary>
    void Start()
    {
        currentHP = hpIcons.Count; /* HPアイコンの数を取得 */
        invincibilityController = GetComponent<InvincibilityController>();
    }

    /// <summary>
    /// プレイヤーが敵に接触した際にダメージ処理を呼び出します。
    /// </summary>
    /// <param name="collision">衝突情報</param>
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            TakeDamage(); /* ダメージを受けるメソッドを呼ぶ */
        }
    }

    /// <summary>
    /// プレイヤーにダメージを与える処理
    /// </summary>
    private void TakeDamage()
    {
        if (currentHP > 0 && !invincibilityController.IsInvincible())
        {
            currentHP--;
            GameData.playerHP = currentHP;
            hpIcons[currentHP].enabled = false; /* HPアイコンを非表示にする */
            AudioManager.Instance.PlaySE("sePlayerDamage"); /* SEを再生 */
            invincibilityController.StartInvincibility(); /* 無敵化開始 */

            if (currentHP <= 0)
            {
                gameObject.SetActive(false);
                SceneController.Instance.LoadResult();
            }
        }

    }
}
