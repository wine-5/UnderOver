using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundFlipper : MonoBehaviour
{

    [Header("GroundFlipperの設定")]
    [SerializeField] private LayerMask groundLayer; /* 地面用のレイヤーの設定 */

    [SerializeField] private float rayLength = 5.0f; /* Rayの長さを設定する変数 */
    [SerializeField] private GameObject playerObject; /* プレイヤーのGameObjectを入れる変数 */

    private Transform playerTransform; /* プレイヤーのTransformを入れる変数 */
    private Rigidbody2D rb; /* PlayerControllerのRigidbody2Dを入れる変数 */

    /* フラグ設定 */
    public bool isGroundFlip = false; /* 地面が反転しているかどうか */

    void Start()
    {
        if (playerObject != null)
        {
            playerTransform = playerObject.transform; /* プレイヤーのTransformを取得 */
            rb = playerObject.GetComponent<Rigidbody2D>(); /* プレイヤーのRigidbody2Dを取得 */
        }
        else
        {
            Debug.LogError("PlayerObjectが設定されていない");
        }


    }

    void Update()
    {
        Debug.DrawRay(playerTransform.position, Vector2.up * rayLength, Color.red); /* Raycastの可視化 */

        if (Input.GetMouseButtonDown(0) && !isGroundFlip) /* 左クリックを押したとき */
        {
            FlipGround(); /* Playerの位置を逆向きにする */
            Debug.Log(isGroundFlip);
        }
        else if (Input.GetMouseButtonDown(0) && isGroundFlip) /* 地面が反転しているとき */
        {
            isGroundFlip = false; /* 地面が反転していない状態に戻す */
            rb.gravityScale = 1; /* 重力を有効にする */

            Debug.Log(isGroundFlip);

        }
    }

    private void FlipGround()
    {
        isGroundFlip = true; /* 地面が反転している */

        if (playerTransform == null)
        {
            Debug.LogError("PlayerTransformがnull");
            return;
        }

        /* Playerの真上をRayで探す */
        RaycastHit2D hit = Physics2D.Raycast(playerTransform.position, Vector2.up, Mathf.Infinity, groundLayer); /* Raycastを使って地面の情報を取得 */

        if (hit.collider != null) /* Raycastが地面に当たった場合 */
        {
            Vector2 newPosition = hit.point; /* Raycastが当たった地面の位置を取得 */
            Debug.Log(newPosition); /* デバッグ用に地面の位置を表示 */
            newPosition.y -= 0.1f; /* めり込みするのを防止するために少し下に設定 */

            playerTransform.position = newPosition; /* Playerの位置を地面の位置に設定 */
            playerTransform.rotation = Quaternion.Euler(0, 0, 180); /* Playerの向きを反転させる */
        }

        /* ここから下の地面に落ちないように重力を無効にする */
        rb.gravityScale = 0; /* 重力を無効にする */

    }

    /* 地面と接触しているかを確認するメソッド */
    private void CheckGroundedStatus()
    {

    }
}
