using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// ゲームのシーン管理を行います。ステージ遷移やBGMの変更などを担当
/// </summary>
public class SceneController : MonoBehaviour
{
    /* インスタンスを作成 */
    public static SceneController Instance { get; private set; }

    /* Stageの基本設定 */
    public int currentStage { get; private set; } = 1;

    private const int MAX_STAGE = 3;

    /// <summary>
    /// SceneControllerのインスタンスを管理
    /// </summary>
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            // Debug.Log("既にInstanceが作成されている。");
            return;
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); /* シーンを跨いでも情報を保持する */
        }
    }

    /// <summary>
    /// 指定したステージ番号のシーンを読み込むと共に、対応するBGMを再生
    /// </summary>
    /// <param name="stageNumber">ステージ番号</param>    
    public void LoadStage(int stageNumber)
    {
        currentStage = stageNumber;
        /* ステージに応じたBGMを再生 */
        switch (stageNumber)
        {
            case 1:
                AudioManager.Instance.PlayBGM("Stage1");
                break;
            case 2:
                AudioManager.Instance.PlayBGM("Stage2");
                break;
            case 3:
                AudioManager.Instance.PlayBGM("Stage3");
                break;
            default:
                AudioManager.Instance.PlayBGM("Title");
                break;
        }

        SceneManager.LoadScene("Stage" + stageNumber);
    }


    /// <summary>
    /// 次のステージを読み込むメソッド
    /// 最大ステージに達している場合はタイトル画面に戻る
    /// </summary>
    public void LoadNextStage()
    {
        if (currentStage < MAX_STAGE)
        {
            LoadStage(currentStage + 1);
        }
        else
        {
            LoadTitle();
        }
    }

    /* もう一度同じシーンを読み込むメソッド */

    /// <summary>
    /// 現在のステージを再読み込み
    /// </summary>
    public void ReloadCurrentStage()
    {
        LoadStage(currentStage);
    }

    /* リザルトシーンに遷移するメソッド */

    /// <summary>
    /// リザルトシーンに遷移
    /// </summary>
    public void LoadResult()
    {
        SceneManager.LoadScene("Result");
    }

    /* タイトルシーンへ遷移するメソッド */
    /// <summary>
    /// タイトルシーンに遷移します。
    /// </summary>
    public void LoadTitle()
    {
        SceneManager.LoadScene("Title");
    }
}