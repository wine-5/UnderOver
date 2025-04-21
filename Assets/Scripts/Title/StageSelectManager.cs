using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSelectManager : MonoBehaviour
{

    public GameObject stageSelectPanal; /* ステージセレクトのパネルのオブジェクトを入れる変数 */
    public GameObject stageSelectButton; /* ステージセレクトボタンを入れる変数 */
    void Start()
    {
        AudioManager.Instance.PlayBGM("Title");
        stageSelectPanal.SetActive(false); /* 最初は非表示 */
    }

    public void ShowStageSelect()
    {
        stageSelectPanal.SetActive(true); /* ここで表示する */
        stageSelectButton.SetActive(false); /* ステージセレクトボタンは非表示にする */
    }

    /* ステージ選択ボタンが押されたときの処理 */
    public void StartStage(int stageNumber)
    {
        SceneController.Instance.LoadStage(stageNumber);
    }

    public void CloseStageSelect()
    {
        stageSelectPanal.SetActive(false); /* 非表示 */
    }

}
