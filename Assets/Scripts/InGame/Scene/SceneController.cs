using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneNames
{
    public const string Title = "Title";
    public const string Result = "Result";
    public const string Stage1 = "Stage1";
    public const string Stage2 = "Stage2";
    public const string Stage3 = "Stage3";
}

public class SceneController : MonoBehaviour
{
    public static SceneController Instance { get; private set; } /* シングルトン */

    private uint currentStage = 0;
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            // DontDestroyOnLoad(gameObject); これを消すとタイトルへ複数回行ける
        }
    }

    public void GoToResultScene()
    {
        SceneManager.LoadScene(SceneNames.Result);
        Debug.Log("リザルトシーンに移動する");
    }
    public void GoToNextStage()
    {
        currentStage++;
        Debug.Log("今のステージは" + currentStage);

        string nextSceneName = "";
        switch (currentStage)
        {
            case 1:
                nextSceneName = SceneNames.Stage1;
                break;
            case 2:
                nextSceneName = SceneNames.Stage2;
                break;
            case 3:
                nextSceneName = SceneNames.Stage3;
                break;
            default:
                Debug.Log("次のステージはもうない");
                return; /* 処理を終了 */
        }

        /* 次のステージに移動する処理 */
        SceneManager.LoadScene(nextSceneName);
    }

    public void GoToTitleScene()
    {
        SceneManager.LoadScene(SceneNames.Title);
    }
}