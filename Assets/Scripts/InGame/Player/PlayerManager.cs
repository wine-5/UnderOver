using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    /* プレイヤーのHP */
    [SerializeField] private int currentHP = 3;
    public List<Image> hpIcons; /* HPアイコンを入れる変数 */

    /* Playerを入れる変数 */
    public GameObject player;

    /* InvincibilityController（無敵状態のスクリプト）*/
    [SerializeField] private InvincibilityController invincibilityController;

    void Start()
    {
        // Debug.Log("今のPlayerのHP" + currentHP);
        currentHP = hpIcons.Count; /* HPアイコンの数を取得 */
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            TakeDamage(); /* 攻撃を受けたときの処理 */
        }
    }

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
                player.SetActive(false);
                SceneController.Instance.LoadResult();
            }
        }

    }
}
