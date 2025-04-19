using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingSpikes : MonoBehaviour
{
    /* 縦方向か横方向 */
    public enum Direction
    {
        UpDown,
        LeftRight,
    }

    /* とげの基本設定 */
    [Header("とげの基本設定")]
    [SerializeField] private Direction moveDirection = Direction.UpDown;
    [SerializeField] private float moveDistance = 2f;
    [SerializeField] float moveSpeed = 2f;
    private Vector3 startPos;
    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float offset = Mathf.Sin(Time.time * moveSpeed) * moveDistance;

        if(moveDirection == Direction.UpDown)
        {
            transform.position = new Vector3(startPos.x, startPos.y + offset, startPos.z);
        }
        else if(moveDirection == Direction.LeftRight)
        {
            transform.position = new Vector3(startPos.x + offset, startPos.y, startPos.z);
        }
    }
}
