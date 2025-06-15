using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーが地面を反転して移動する処理を制御する
/// </summary>
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

    /* 定数の設定 */
    private const float INVERT_ROTATION = 180.0f;


    /// <summary>
    /// コンポーネントの初期化処理を行う
    /// </summary>
    void Start()
    {
        if (playerObject != null)
        {
            playerTransform = playerObject.transform; /* プレイヤーのTransformを取得 */
            rb = playerObject.GetComponent<Rigidbody2D>(); /* プレイヤーのRigidbody2Dを取得 */
        }
    }

    /// <summary>
    /// 左クリック入力を監視し、地面反転処理を実行する
    /// </summary>
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isGroundFlip) /* 左クリックを押したとき */
        {
            FlipGround(); /* Playerの位置を逆向きにする */
        }
        else if (Input.GetMouseButtonDown(0) && isGroundFlip) /* 地面が反転しているとき */
        {
            ResetGround();
        }
    }

    /// <summary>
    /// プレイヤーが地面を反転する処理
    /// </summary>
    private void FlipGround()
    {
        isGroundFlip = true; /* 地面が反転している */

        Vector3 currentRotation = playerTransform.eulerAngles;
        transform.rotation = Quaternion.Euler(currentRotation.x, currentRotation.y + INVERT_ROTATION, currentRotation.z + INVERT_ROTATION); /* Playerの向きを反転させる */

        /* Playerの真上をRayで探す */
        RaycastHit2D hit = Physics2D.Raycast(playerTransform.position, Vector2.up, Mathf.Infinity, groundLayer); /* Raycastを使って地面の情報を取得 */

        if (hit.collider != null) /* Raycastが地面に当たった場合 */
        {
            float offset = GetPlayerColliderHeight() / 2f;
            Vector2 newPosition = hit.point; /* Raycastが当たった地面の位置を取得 */

            newPosition.y -= offset; /* めり込みするのを防止するために少し下に設定 */

            playerTransform.position = newPosition; /* Playerの位置を地面の位置に設定 */
        }

        AudioManager.Instance.PlaySE("seGroundFlip"); /* SEを再生 */

        rb.velocity = Vector2.zero; // 速度リセットで反動防止
        rb.gravityScale = -1; /* 重力を逆にする */
    }

    /// <summary>
    /// プレイヤーの高さを取得して、地形へのめり込みを防ぐ
    /// </summary>
    private void ResetGround()
    {
        isGroundFlip = false;

        Vector3 currentRotation = playerTransform.eulerAngles;
        transform.rotation = Quaternion.Euler(currentRotation.x, currentRotation.y + INVERT_ROTATION, currentRotation.z + 180f);

        RaycastHit2D hit = Physics2D.Raycast(playerTransform.position, Vector2.down, Mathf.Infinity, groundLayer);
        if (hit.collider != null) /* Raycastが地面に当たった場合 */
        {
            float offset = GetPlayerColliderHeight() / 2f;
            Vector2 newPosition = hit.point; /* Raycastが当たった地面の位置を取得 */

            newPosition.y += offset; /* めり込みするのを防止するために少し下に設定 */

            playerTransform.position = newPosition; /* Playerの位置を地面の位置に設定 */
        }

        AudioManager.Instance.PlaySE("seGroundFlip"); /* SEを再生 */

        rb.velocity = Vector2.zero; // 速度リセットで反動防止
        rb.gravityScale = 1;
    }

    /// <summary>
    /// とげにめり込まないためにPlayerの高さを調節する
    /// </summary>
    /// <returns></returns>
    private float GetPlayerColliderHeight()
    {
        Collider2D col = playerObject.GetComponent<Collider2D>();
        if (col != null)
        {
            return col.bounds.size.y;
        }
        return 1.0f;
    }
}
