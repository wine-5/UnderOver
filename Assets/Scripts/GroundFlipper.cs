using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundFlipper : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayer; /* 地面用のレイヤーの設定 */

    [SerializeField] private GameObject playerObject; /* プレイヤーのGameObjectを入れる変数 */
    private Transform playerTransform; /* プレイヤーのTransformを入れる変数 */
    void Start()
    {
        if (playerObject != null)
        {
            playerTransform = playerObject.transform; /* プレイヤーのTransformを取得 */
        }
        else
        {
            Debug.LogError("PlayerObjectが設定されていない");
        }

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            FlipGround(); /* Playerの位置を逆向きにする */
        }
    }

    private void FlipGround()
    {
        if (playerTransform == null)
        {
            Debug.LogError("PlayerTransformがnull");
            return;
        }

        /* Playerの真上をRayで探す */
        RaycastHit2D hit = Physics2D.Raycast(playerTransform.position,Vector2.up, Mathf.Infinity, groundLayer); /* Raycastを使って地面の情報を取得 */

        if(hit.collider != null) /* Raycastが地面に当たった場合 */
        {
            Vector2 newPosition = hit.collider.transform.position; /* Raycastが当たった地面の位置   を取得 */
            newPosition.y -= 0.1f; /* めり込みするのを防止するために少し下に設定 */

            playerTransform.position = newPosition; /* Playerの位置を地面の位置に設定 */
            playerTransform.rotation = Quaternion.Euler(0, 0, 180); /* Playerの向きを反転させる */
        }
    }
}
