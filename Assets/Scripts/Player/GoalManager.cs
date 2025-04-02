using UnityEngine;

public class GoalManager : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        IGoalable goalable = other.GetComponent<IGoalable>();
        if(goalable != null)
        {
            goalable.OnGoalReached(); /* Playerがゴールした時に呼び出す */
        }
    }
}
