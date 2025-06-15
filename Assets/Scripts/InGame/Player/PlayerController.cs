using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

/// <summary>
/// プレイヤーの移動とジャンプを制御
/// </summary>
public class PlayerController : MonoBehaviour
{

    [Header("移動速度とジャンプ力")]
    [SerializeField] private float moveSpeed = 5.0f;
    [SerializeField] private float jumpPower = 5.0f;
    private Rigidbody2D rb;
    private Vector2 moveInput;

    /* フラグの設定 */
    public bool isGrounded = true;

    [SerializeField] private GroundFlipper groundFlipper; /* GroundFlipperのスクリプトを入れる変数 */
    // --- 定数定義 ---
    private const float DIRECTION_RIGHT = 1f;
    private const float DIRECTION_LEFT = -1f;
    private const float DIRECTION_NONE = 0f;
    private const float ROTATION_Y_RIGHT = 180f;
    private const float ROTATION_Y_LEFT = 0f;
    private const float ROTATION_Z_NORMAL = 0f;
    private const float ROTATION_Z_FLIPPED = 180f;
    private const string GROUND_TAG = "Ground";

    /// <summary>
    /// 初期設定。Rigidbody2Dを取得し、GroundFlipperの設定を確認
    /// </summary>
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// プレイヤーの移動処理と方向転換処理を更新
    /// </summary>
    void Update()
    {
        UpdateDirection();
        MovePlayer();
    }


    /// <summary>
    /// プレイヤーの移動入力を取得
    /// </summary>
    /// <param name="context">入力の状態</param>
    public void OnMove(InputAction.CallbackContext context)
    {
        // Debug.Log("Move: " + context.ReadValue<Vector2>());
        moveInput = context.ReadValue<Vector2>();
    }

    /// <summary>
    /// プレイヤーの移動処理を行う
    /// </summary>
    private void MovePlayer()
    {
        rb.velocity = new Vector2(moveInput.x * moveSpeed, rb.velocity.y);
    }

    /// <summary>
    /// プレイヤーの向きを地面の状態に応じて更新
    /// </summary>
    private void UpdateDirection()
    {
        bool isGroundFlip = groundFlipper.isGroundFlip; /* GroundFlipperのスクリプトを取得 */

        if (!isGroundFlip) /* 下の地面 */
        {
            if (moveInput.x >= DIRECTION_RIGHT) /* 右に進んでいる */
            {
                transform.rotation = Quaternion.Euler(0, ROTATION_Y_RIGHT, ROTATION_Z_NORMAL);
            }
            else if (moveInput.x <= DIRECTION_LEFT) /* 左に進んでいる */
            {
                transform.rotation = Quaternion.Euler(0, ROTATION_Y_LEFT, ROTATION_Z_NORMAL);
            }
            else
            {
                rb.velocity = new Vector2(DIRECTION_NONE, rb.velocity.y);
            }
        }
        else if (isGroundFlip) /* 上の地面 */
        {
            if (moveInput.x >= DIRECTION_RIGHT) /* 右に進んでいる */
            {
                transform.rotation = Quaternion.Euler(0, ROTATION_Y_LEFT, ROTATION_Z_FLIPPED);
            }
            else if (moveInput.x <= DIRECTION_LEFT) /* 左に進んでいる */
            {
                transform.rotation = Quaternion.Euler(0, ROTATION_Y_RIGHT, ROTATION_Z_FLIPPED);
            }
            else
            {
                rb.velocity = new Vector2(DIRECTION_NONE, rb.velocity.y);
            }
        }
    }


    /// <summary>
    /// プレイヤーがジャンプしたときの処理
    /// </summary>
    /// <param name="context">入力の状態</param>
    public void OnJump(InputAction.CallbackContext context)
    {
        bool isGroundFlip = groundFlipper.isGroundFlip; /* GroundFlipperのスクリプトを取得 */

        if (isGrounded) /* どちらかの地面にいるとき */
        {
            // Debug.Log("今とんだ");
            float jumpDirection = isGroundFlip ? DIRECTION_LEFT : DIRECTION_RIGHT; /* 上の地面なら下に、下の地面なら上に */
            rb.velocity = Vector2.zero; /* ジャンプをリセット */
            rb.velocity = new Vector2(rb.velocity.x, jumpPower * jumpDirection); /* ジャンプする */

            AudioManager.Instance.PlaySE("seJump"); /* SEを再生 */
            isGrounded = false;
        }
    }


    /// <summary>
    /// プレイヤーが地面に接触した際の処理
    /// </summary>
    /// <param name="collision">衝突情報</param>
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == GROUND_TAG)
        {
            isGrounded = true;
        }
    }
}
