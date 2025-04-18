using UnityEngine;

public class GoalManager : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("ゴールに到達した！");

        if (SceneController.Instance != null)
        {
            SceneController.Instance.GoToResultScene();
        }
        else
        {
            Debug.Log("SceneControllerのInstanceがnullです");
        }
    }
}
