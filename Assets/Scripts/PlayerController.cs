using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    [Header("移動速度とジャンプ力")]
    [SerializeField] private float moveSpeed = 5.0f;
    [SerializeField] private float jumpPower = 10.0f;
    public Rigidbody2D rb;

    private Vector2 moveInput;

    /* フラグの設定 */
    public bool isGrounded = true;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        UpdateDirection();
        MovePlayer();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Debug.Log("Move: " + context.ReadValue<Vector2>());
        moveInput = context.ReadValue<Vector2>();



    }

    private void MovePlayer()
    {
        rb.velocity = new Vector2(moveInput.x * moveSpeed, rb.velocity.y);

    }

    private void UpdateDirection()
    {

        // Debug.Log(moveInput);
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

    public void OnJump(InputAction.CallbackContext context)
    {
        if (isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
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
