using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    /* プレイヤーのHP */
    [SerializeField] private int currentHP = 3;

    /* Playerを入れる変数 */
    public GameObject player;
    void Start()
    {
        Debug.Log("今のPlayerのHP" + currentHP);
    }

    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            currentHP--;
            Debug.Log("攻撃を受けた後のPlayerのHP" + currentHP);
            if(currentHP <= 0)
            {
                player.SetActive(false);
            }
        }
    }
}
