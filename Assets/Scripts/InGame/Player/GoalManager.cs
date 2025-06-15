using UnityEngine;

/// <summary>
/// ゴールに到達した際の処理を管理する
/// </summary>
public class GoalManager : MonoBehaviour
{
    /// <summary>
    /// トリガーに入ったオブジェクトがプレイヤーかどうかを判定し、ゴール達成の処理を行う
    /// </summary>
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Debug.Log("ゴールに到達した！");

        if (SceneController.Instance != null)
        {
            AudioManager.Instance.PlaySE("seGoal"); /* SEを再生 */

            SceneController.Instance.LoadResult();
        }
    }
}
