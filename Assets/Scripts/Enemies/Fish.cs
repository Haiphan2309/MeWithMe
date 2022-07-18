using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : Enemy
{
    public float speed;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isDie==false)
            rb.velocity = new Vector2(speed, 0) * Time.fixedDeltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "EnemyAttack") GetHit(3);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Attack") GetHit(1);
        if (collision.gameObject.tag == "SmallAttack") GetHit(0.5f);
        speed = -speed;
        transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
    }
}
