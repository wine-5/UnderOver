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

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    public void GoToResultScene()
    {
        SceneManager.LoadScene(SceneNames.Result);
        Debug.Log("リザルトシーンに移動する");
    }
    public void GoToNextStage()
    {
        SceneManager.LoadScene(SceneNames.Stage2);
    }

    public void GoToTitleScene()
    {
        SceneManager.LoadScene(SceneNames.Title);
    }
}