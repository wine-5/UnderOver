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
        currentHP--;
        // Debug.Log("攻撃を受けた後のPlayerのHP" + currentHP);
        hpIcons[currentHP].enabled = false; /* HPアイコンを非表示にする */
        if (currentHP <= 0)
        {
            player.SetActive(false);
        }
    }
}
