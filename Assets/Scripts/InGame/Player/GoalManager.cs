using UnityEngine;

public class GoalManager : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Debug.Log("ゴールに到達した！");

        if (SceneController.Instance != null)
        {
            AudioManager.Instance.PlaySE("seGoal"); /* SEを再生 */

            SceneController.Instance.LoadResult();
        }
        else
        {
            // Debug.Log("SceneControllerのInstanceがnullです");
        }
    }
}
