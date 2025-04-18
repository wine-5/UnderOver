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
    private int currentStage = 0;

    private readonly string[] stageSceneNames = {
        SceneNames.Stage1,
        SceneNames.Stage2,
        SceneNames.Stage3,
    };
    
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); /* すでにInstanceが存在した場合は新しいInstanceを削除する */
            Debug.Log("すでにSceneControllerが存在した。新しいInstanceを削除した");
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); /* タイトルシーン以降も使用できるように */
            Debug.Log("SceneControllerにInstanceを設定した");
        }
    }

    void Start()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        for(int i = 0; i < stageSceneNames.Length; i++)
        {
            if(stageSceneNames[i] == currentSceneName)
            {
                currentStage = i;
                break;
            }
        }
    }

    public void GoToResultScene()
    {
        SceneManager.LoadScene(SceneNames.Result);
        // Debug.Log("リザルトシーンに移動する");
    }
    public void GoToNextStage()
    {
        currentStage++;

        if(currentStage < stageSceneNames.Length)
        {
            string nextSceneName = stageSceneNames[currentStage];
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            Debug.Log("全ステージクリア！");
        }
    }

    public void GoToTitleScene()
    {
        SceneManager.LoadScene(SceneNames.Title);
    }

    public void RetryScene()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }

    
}