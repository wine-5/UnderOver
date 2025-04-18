using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    /* インスタンスを作成 */
   public static SceneController Instance {get; private set;}

   /* Stageの基本設定 */
   public int currentStage{get; private set;} = 1;

   private const int MAX_STAGE = 3;

    void Awake()
    {
        if(Instance != null)
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

    public void LoadStage(int stageNumber)
    {
        currentStage = stageNumber;
        SceneManager.LoadScene("Stage" + stageNumber);
    }

    /* 次のステージをロードするメソッド */
    public void LoadNextStage()
    {
        if(currentStage < MAX_STAGE)
        {
            LoadStage(currentStage + 1);
        }
        else
        {
            LoadTitle();
        }
    }

    /* もう一度同じシーンを読み込むメソッド */
    public void ReloadCurrentStage()
    {
        LoadStage(currentStage);
    }

    /* リザルトシーンに遷移するメソッド */
    public void LoadResult()
    {
        SceneManager.LoadScene("Result");
    }

    /* タイトルシーンへ遷移するメソッド */
    public void LoadTitle()
    {
        SceneManager.LoadScene("Title");
    }
}