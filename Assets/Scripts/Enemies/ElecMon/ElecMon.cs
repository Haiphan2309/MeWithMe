using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElecMon : Enemy
{
    public LayerMask groundLayer;

    public int direct;
    public float speed;
    public bool[] checkDirectArr = new bool[4];
    public bool[] smallCheckDirectArr = new bool[4];

    public bool isChangeDirect, isClockwise;

    float size;
    // Start is called before the first frame update
    void Start()
    {
        size = transform.localScale.z / 2;
    }

    void FixedUpdate()
    {
        if (isDie == false)
        {
            SetDirectArr();
            Move();
        }
    }

    void SetDirectArr()
    {
        checkDirectArr[0] = Physics2D.OverlapCircle(transform.position + new Vector3(0f, size, 0)*2, size, groundLayer);
        checkDirectArr[1] = Physics2D.OverlapCircle(transform.position + new Vector3(size, 0, 0)*2, size, groundLayer);
        checkDirectArr[2] = Physics2D.OverlapCircle(transform.position + new Vector3(0f, -size, 0)*2, size, groundLayer);
        checkDirectArr[3] = Physics2D.OverlapCircle(transform.position + new Vector3(-size, 0, 0)*2, size, groundLayer);

        smallCheckDirectArr[0] = Physics2D.OverlapCircle(transform.position + new Vector3(0f, size, 0) / 2, size/2, groundLayer);
        smallCheckDirectArr[1] = Physics2D.OverlapCircle(transform.position + new Vector3(size, 0, 0) / 2, size/2, groundLayer);
        smallCheckDirectArr[2] = Physics2D.OverlapCircle(transform.position + new Vector3(0f, -size, 0) / 2, size/2, groundLayer);
        smallCheckDirectArr[3] = Physics2D.OverlapCircle(transform.position + new Vector3(-size, 0, 0) / 2, size/2, groundLayer);
    }

    void Move()
    {
        direct %= 4;

        if (direct == 0) rb.velocity = Vector2.up * speed;
        if (direct == 1) rb.velocity = Vector2.right * speed;
        if (direct == 2) rb.velocity = Vector2.down * speed;
        if (direct == 3) rb.velocity = Vector2.left * speed;

        if (isClockwise)
        {
            if (checkDirectArr[(direct + 1) % 4]) isChangeDirect = false;
            if (checkDirectArr[(direct + 1) % 4] == false) //turn right
            {
                if (isChangeDirect == false)
                {
                    direct++;
                    isChangeDirect = true;
                }
            }
            else if (smallCheckDirectArr[direct]) //turn left
            {
                direct += 3; //tuong duong direct --;
            }
        }
        else
        {
            if (checkDirectArr[(direct + 3) % 4]) isChangeDirect = false;
            if (checkDirectArr[(direct + 3) % 4] == false) //turn right
            {
                if (isChangeDirect == false)
                {
                    direct += 3;
                    isChangeDirect = true;
                }
            }
            else if (smallCheckDirectArr[direct]) //turn left
            {
                direct ++; //tuong duong direct --;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Attack")) GetHit(1);
        if (collision.CompareTag("SmallAttack")) GetHit(0.5f);
        if (collision.CompareTag("EnemyAttack")) GetHit(3);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + new Vector3(0f, size, 0)*2, size);
        Gizmos.DrawWireSphere(transform.position + new Vector3(size, 0, 0)*2, size);
        Gizmos.DrawWireSphere(transform.position + new Vector3(size, 0, 0) /2, size/2);
    }

    void OnBecameVisible()
    {
        enabled = true;
    }
    void OnBecameInvisible()
    {
        enabled = true;
    }
}
