using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// リザルト画面のUIを制御するクラス
/// ゲームクリアまたはゲームオーバー時に適切なUIを表示
/// </summary>
public class ResultUIController : MonoBehaviour
{
    /* クリア / ゲームオーバーの時に表示するオブジェクトを入れる変数 */
    public GameObject clearObj;
    public GameObject gameoverObj;

    /// <summary>
    /// ゲーム開始時に結果UIを表示
    /// </summary>
    void Start()
    {
        ShowResultUI();
    }

    /// <summary>
    /// プレイヤーのHPに応じてクリアまたはゲームオーバーのUIを表示
    /// </summary>
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
