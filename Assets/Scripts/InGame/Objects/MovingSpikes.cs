using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingSpikes : MonoBehaviour
{
    /* 動く方向の設定 */
    public enum Direction
    {
        UpDown,
        LeftRight,
    }
    /* 動く方向の設定 */
    public enum MoveMode
    {
        PingPong, /* 往復 */
        OneWay /* 一方向 */
    }
    /* とげの基本設定 */
    [Header("とげの基本設定")]
    [SerializeField] private Direction moveDirection = Direction.UpDown;
    [SerializeField] private MoveMode moveMode = MoveMode.PingPong; /* 初期値は往復 */
    [SerializeField] private float moveDistance = 2f;
    [SerializeField] float moveSpeed = 2f;
    private Vector3 startPos;
    private float moveTimer = 0f;
    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        moveTimer += Time.deltaTime;
        float offset = 0;

        if(moveMode == MoveMode.PingPong)
        {
            offset = Mathf.Sin(Time.time * moveSpeed) * moveDistance;
        }
        else if(moveMode == MoveMode.OneWay)
        {
            offset = -moveTimer * moveSpeed; /* 一方向に進む（左）*/
            offset = Mathf.Max(offset, -moveDistance);
        }

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
