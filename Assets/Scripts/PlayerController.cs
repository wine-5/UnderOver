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
    private Rigidbody2D rb;

    private Vector2 moveInput;

    /* フラグの設定 */
    private bool isGrounded = true;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        UpdateDirection();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();

        rb.velocity = new Vector2(moveInput.x * moveSpeed, rb.velocity.y);
    }

    private void UpdateDirection()
    {
        Debug.Log(moveInput);
        if(moveInput.x > 0) /* 右に進んでいる */
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if(moveInput.x < 0) /* 左に進んでいる */
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            Debug.Log("移動していない");
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
