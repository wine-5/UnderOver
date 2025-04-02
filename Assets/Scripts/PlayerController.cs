using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    [Header("移動速度とジャンプ力")]
    [SerializeField] private float moveSpeed = 5.0f;
    [SerializeField] private float jumpPower = 5.0f;
    public Rigidbody2D rb;

    private Vector2 moveInput;

    /* フラグの設定 */
    public bool isGrounded = true;

    [SerializeField] private GroundFlipper groundFlipper; /* GroundFlipperのスクリプトを入れる変数 */
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (groundFlipper == null)
        {
            // Debug.LogError("GroundFlipperが設定されていない");
        }
    }

    void Update()
    {
        UpdateDirection();
        MovePlayer();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        // Debug.Log("Move: " + context.ReadValue<Vector2>());
        moveInput = context.ReadValue<Vector2>();



    }

    private void MovePlayer()
    {
        rb.velocity = new Vector2(moveInput.x * moveSpeed, rb.velocity.y);

    }

    private void UpdateDirection()
    {
        bool isGroundFlip = groundFlipper.isGroundFlip; /* GroundFlipperのスクリプトを取得 */

        if (!isGroundFlip) /* 下の地面 */
        {
            if (moveInput.x >= 1) /* 右に進んでいる */
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            else if (moveInput.x <= -1) /* 左に進んでいる */
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
        }
        else if (isGroundFlip) /* 上の地面 */
        {
            if (moveInput.x >= 1) /* 右に進んでいる */
            {
                transform.rotation = Quaternion.Euler(0, 0, 180);
            }
            else if (moveInput.x <= -1) /* 左に進んでいる */
            {
                transform.rotation = Quaternion.Euler(0, 180, 180);
            }
            else
            {
                rb.velocity = new Vector2(0, rb.velocity.y); 
            }
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        bool isGroundFlip = groundFlipper.isGroundFlip; /* GroundFlipperのスクリプトを取得 */

        if (isGrounded || isGroundFlip) /* どちらかの地面にいるとき */
        {
            float jumpDirection = isGroundFlip ? -1 : 1; /* 上の地面なら下に、下の地面なら上に */
            rb.velocity = Vector2.zero; /* ジャンプをリセット */
            rb.velocity = new Vector2(rb.velocity.x, jumpPower * jumpDirection); /* ジャンプする */

            Debug.Log(isGrounded);
            
            isGrounded = false;
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }
}
