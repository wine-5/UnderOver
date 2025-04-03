using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController Instance {get; private set; } /* シングルトン */

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
        SceneManager.LoadScene("Result");
        Debug.Log("リザルトシーンに移動する");
    }
    public void GoToNextStage()
    {
        SceneManager.LoadScene("Stage2");
    }

    public void GoToTitleScene()
    {
        SceneManager.LoadScene("Title");
    }
}