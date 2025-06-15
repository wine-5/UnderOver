using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// タイトル画面でのステージ選択UIを管理
/// </summary>
public class StageSelectManager : MonoBehaviour
{
    [SerializeField] private GameObject homePanel; /* ステージセレクトボタンを入れる変数 */

    [SerializeField] private GameObject stageSelectPanal; /* ステージセレクトのパネルのオブジェクトを入れる変数 */

    /// <summary>
    /// 初期化処理。BGM再生とステージ選択パネルの非表示化。
    /// </summary>
    void Start()
    {
        AudioManager.Instance.PlayBGM("Title");
        stageSelectPanal.SetActive(false); /* 最初は非表示 */
    }

    /// <summary>
    /// ステージ選択パネルを表示し、ホームパネルを非表示にする。
    /// </summary>
    public void ShowStageSelect()
    {
        AudioManager.Instance.PlaySE("seClickButton"); /*SEを再生 */

        stageSelectPanal.SetActive(true); /* ここで表示する */
        homePanel.SetActive(false); /* ステージセレクトボタンは非表示にする */
    }

    /* ステージ選択ボタンが押されたときの処理 */

    /// <summary>
    /// 指定されたステージを開始する。
    /// </summary>
    /// <param name="stageNumber">開始するステージ番号</param>
    public void StartStage(int stageNumber)
    {
        AudioManager.Instance.PlaySE("seClickButton"); /*SEを再生 */

        SceneController.Instance.LoadStage(stageNumber);
    }

    /// <summary>
    /// ステージ選択パネルを閉じる。
    /// </summary>
    public void CloseStageSelect()
    {
        stageSelectPanal.SetActive(false); /* 非表示 */
    }

}
