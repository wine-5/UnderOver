using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultUIController : MonoBehaviour
{
    /* クリア / ゲームオーバーの時に表示するオブジェクトを入れる変数 */
    public GameObject clearObj;
    public GameObject gameoverObj;

    void Start()
    {
        ShowResultUI();
    }

    public void ShowResultUI()
    {
        // Debug.Log($"Playerの残機は：{GameData.playerHP}");
        if (GameData.playerHP > 0) /* クリア */
        {
            AudioManager.Instance.PlayBGM("Clear");
            clearObj.gameObject.SetActive(true);
            gameoverObj.gameObject.SetActive(false);
        }
        else /* ゲームオーバー */
        {
            AudioManager.Instance.PlayBGM("GameOver", 2.0f); /* 音量を2倍で渡す */

            clearObj.gameObject.SetActive(false);
            gameoverObj.gameObject.SetActive(true);
        }
    }
}
